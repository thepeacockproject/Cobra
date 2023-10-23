using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Cobra.Server.Database;
using Cobra.Server.Edm.Json;
using Cobra.Server.Hitman.Interfaces;
using Cobra.Server.Hitman.Services;
using Cobra.Server.Interfaces;
using Cobra.Server.Mvc;
using Cobra.Server.Services;
using Cobra.Server.Shared.Interfaces;
using Cobra.Server.Shared.Models;
using Cobra.Server.Sniper.Interfaces;
using Cobra.Server.Sniper.Services;
using Microsoft.EntityFrameworkCore;

namespace Cobra.Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddConsole();
            });

            services
                .AddMvc(options =>
                {
                    options.Filters.Add<ModelStateFilter>();

                    options.EnableEndpointRouting = false;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;

                    //NOTE: The in-game JSON Deserializer expects the actual quote-character, not the escaped unicode!
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new BooleanToStringConverter());
                    options.JsonSerializerOptions.Converters.Add(new FloatToStringConverter());
                    options.JsonSerializerOptions.Converters.Add(new IntegerToStringConverter());
                });

            var options = _configuration
                .GetSection("Options")
                .Get<Options>();

            services.AddSingleton(options);

            services.AddDbContext<DatabaseContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(_configuration.GetConnectionString("CobraDatabase"));
            });

            services.AddSingleton<ISimpleLogger>(_ => new SimpleLogger("Data"));

            services.AddSingleton<IHitmanMetadataService, HitmanMetadataService>();
            services.AddSingleton<ISniperMetadataService, SniperMetadataService>();

            services.AddTransient<FixAddMetricsContentTypeMiddleware>();
            services.AddTransient<RequestResponseLoggerMiddleware>();
            services.AddTransient<SteamAuthMiddleware>();

            services.AddSingleton<IContractsService, ContractsService>();

            if (options.UseCustomContracts)
            {
                services.AddSingleton<IHitmanServer, LocalHitmanServer>();
            }
            else
            {
                services.AddSingleton<IHitmanServer, MockedHitmanServer>();
            }

            services.AddSingleton<ISniperServer, MockedSniperServer>();

            if (options.SteamService == Options.ESteamService.GameServer)
            {
                services.AddSingleton<ISteamService, SteamGameServerService>();
            }
            else if (options.SteamService == Options.ESteamService.WebApi)
            {
                services.AddSingleton<ISteamService, SteamWebApiService>();
            }
        }

        public void Configure(
            IApplicationBuilder app,
            Options options,
            IHitmanServer hitmanServer,
            ISniperServer sniperServer,
            DatabaseContext databaseContext
        )
        {
            databaseContext.Database.Migrate();

            hitmanServer.Initialize();
            sniperServer.Initialize();

            if (options.FixAddMetricsContentType)
            {
                app.UseMiddleware<FixAddMetricsContentTypeMiddleware>();
            }

            if (options.EnableRequestLogging)
            {
                app.UseMiddleware<RequestResponseLoggerMiddleware>();
            }

            if (options.SteamService != Options.ESteamService.None)
            {
                app.UseMiddleware<SteamAuthMiddleware>();
            }

            app.UseMvc();
        }
    }
}

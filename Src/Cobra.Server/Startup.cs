using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Cobra.Server.Edm.Json;
using Cobra.Server.Interfaces;
using Cobra.Server.Mvc;
using Cobra.Server.Services;
using Cobra.Server.Shared.Interfaces;
using Cobra.Server.Shared.Models;
using DatabaseCompositions = Cobra.Server.Database.Compositions;
using HitmanCompositions = Cobra.Server.Hitman.Compositions;
using SniperCompositions = Cobra.Server.Sniper.Compositions;

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

            services.AddHttpContextAccessor();

            var options = _configuration
                .GetSection("Options")
                .Get<Options>();

            services.AddSingleton(options);

            //Shared
            services.AddSingleton<ISimpleLogger>(_ => new SimpleLogger("Data"));
            services.AddSingleton<IUserService, UserService>();

            switch (options.SteamService)
            {
                case Options.ESteamService.GameServer:
                    services.AddSingleton<ISteamService, SteamGameServerService>();
                    break;
                case Options.ESteamService.WebApi:
                    services.AddSingleton<ISteamService, SteamWebApiService>();
                    break;
            }

            //Middleware
            services.AddTransient<FixAddMetricsContentTypeMiddleware>();
            services.AddTransient<RequestResponseLoggerMiddleware>();
            services.AddTransient<SteamAuthMiddleware>();

            //Compositions
            DatabaseCompositions.ConfigureServices(services, _configuration, options);
            HitmanCompositions.ConfigureServices(services, _configuration, options);
            SniperCompositions.ConfigureServices(services, _configuration, options);
        }

        public void Configure(
            IApplicationBuilder app,
            IServiceProvider services,
            Options options
        )
        {
            DatabaseCompositions.Configure(services);
            HitmanCompositions.Configure(services);
            SniperCompositions.Configure(services);

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

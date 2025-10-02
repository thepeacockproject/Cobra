using Cobra.Server.Edm.Interfaces;
using Cobra.Server.Hitman.Interfaces;
using Cobra.Server.Hitman.Services;
using Cobra.Server.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cobra.Server.Hitman
{
    public static class Compositions
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration, Options options)
        {
            services.AddKeyedSingleton<IMetadataService, HitmanMetadataService>(Constants.SchemaNamespace);

            switch (options.GameService)
            {
                case Options.EGameService.Mocked:
                    services.AddSingleton<IHitmanServer, MockedHitmanServer>();
                    break;

                case Options.EGameService.Local:
                    services.AddSingleton<IHitmanServer, LocalHitmanServer>();
                    services.AddSingleton<IContractsService, LocalContractsService>();
                    break;

                case Options.EGameService.Public:
                    services.AddSingleton<IHitmanServer, HitmanServer>();
                    services.AddSingleton<IHitmanUserService, HitmanUserService>();
                    break;
            }
        }

        public static void Configure(IServiceProvider services)
        {
            var hitmanServer = services.GetRequiredService<IHitmanServer>();
            hitmanServer.Initialize();
        }
    }
}

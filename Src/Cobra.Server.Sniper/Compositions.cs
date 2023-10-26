using Cobra.Server.Shared.Models;
using Cobra.Server.Sniper.Interfaces;
using Cobra.Server.Sniper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cobra.Server.Sniper
{
    public static class Compositions
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration, Options options)
        {
            services.AddSingleton<ISniperMetadataService, SniperMetadataService>();
            services.AddSingleton<ISniperServer, MockedSniperServer>();
        }

        public static void Configure(IServiceProvider services)
        {
            //Do nothing
        }
    }
}

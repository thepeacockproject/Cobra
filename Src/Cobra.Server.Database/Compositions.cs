using Cobra.Server.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cobra.Server.Database
{
    public static class Compositions
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration, Options options)
        {
            services.AddDbContextFactory<DatabaseContext>(optionsBuilder =>
            {
                optionsBuilder
                    .UseSqlite(
                        configuration.GetConnectionString("CobraDatabase"),
                        x => x.MigrationsAssembly("Cobra.Server.Database")
                    );
            });
        }

        public static void Configure(IServiceProvider services)
        {
            var databaseContext = services.GetRequiredService<DatabaseContext>();
            databaseContext.Database.Migrate();
        }
    }
}

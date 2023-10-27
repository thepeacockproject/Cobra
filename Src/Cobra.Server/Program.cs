using Microsoft.AspNetCore;

namespace Cobra.Server
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.user.json", true);
                })
                .UseKestrel(options =>
                {
                    //NOTE: Since the game doesn't actually POST data...
                    options.Limits.MaxRequestLineSize = int.MaxValue;
                    options.Limits.MaxRequestBufferSize = int.MaxValue;
                })
                .UseStartup<Startup>()
                .Build();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WinService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // Use the .Net Core/.Net defaults including the standard appsettings config handling
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Add the background service as a hosted service
                    services.AddHostedService<Worker>();
                })
                // Use the Windows Service integration modules
                .UseWindowsService();
    }
}

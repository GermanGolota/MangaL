using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MangaLWebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await SeedDataIfNecessary(host);
            host.Run();
        }
   
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseKestrel();
                        webBuilder.UseStartup<Startup>();
                    });
        }
        private static async Task SeedDataIfNecessary(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                if (config.GetValue<bool>("SeedData"))
                {
                    await Seeder.SeedMangas(host);
                    await Seeder.SeedUsers(host);
                }
            }
        }
    }
}

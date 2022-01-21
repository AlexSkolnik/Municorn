using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Municorn.DAL.Contexts;

using Serilog;

namespace Municorn.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Так как у нас тестовый проектик, данный подход допустим
            //Apply migrations at runtime https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=vs
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var logger = host.Services.GetService<ILogger<Program>>();

                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<MunicornDbContext>();
                    if (db.Database.EnsureCreated())
                    {
                        logger.LogInformation("[Database] -> EnsureCreated");
                        db.Database.Migrate();
                        logger.LogInformation("[Database] -> Migrate");
                    }
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Не удалось провести миграцию");
                    logger.LogError(ex, "Не удалось провести миграцию");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}

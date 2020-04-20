using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TweetJournal.Api.StartupConfiguration;

namespace TweetJournal.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ==
                                Environments.Development;
            if (isDevelopment)
            {
                CreateDevelopmentHost(args);
            }
            else
            {
                CreateProductionHost(args);
            }
            CreateHostBuilder(args).Build().Run();
        }

        private static void CreateDevelopmentHost(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseStartup<StartupDevelopment>()
                .Build().Run();
        }
        
        private static void CreateProductionHost(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseStartup<Startup>()
                .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

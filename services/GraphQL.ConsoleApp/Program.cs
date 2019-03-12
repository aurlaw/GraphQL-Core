using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using NHLStats.Data.Hubs;

namespace GraphQL.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

             var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
             var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .Build();

                // IHubContext<NotificationHub> hubContext = null;
                // using (var serviceScope = host.Services.CreateScope())
                // {
                //     var services = serviceScope.ServiceProvider;

                //     try
                //     {
                //         //NotificationHub

                //         hubContext = services.GetRequiredService<IHubContext<NotificationHub>>();
                //         // Use the context here
                //     }
                //     catch (Exception ex)
                //     {
                //         logger.Error(ex, "An error occurred.");
                //     }
                // }

            host.RunAsync();

            try
            {
                // var servicesProvider = BuildDi();
                logger.Info("Starting Hangfire...");
                JobStorage.Current = new SqlServerStorage(configuration.GetConnectionString("HangfireConnection"));
                using (var server = new BackgroundJobServer())
                {
                    // hubContext.Clients.All.SendAsync("broadcastNotification", "Processor", "Started");
                    Console.WriteLine("Hangfire Server started. Press any key to exit...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        // private static IServiceProvider BuildDi()
        // {
        //     var services = new ServiceCollection();

        //     services.AddSingleton<ILoggerFactory, LoggerFactory>();
        //     services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        //     services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));

        //     var serviceProvider = services.BuildServiceProvider();

        //     var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        //     //configure NLog
        //     loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
        //     NLog.LogManager.LoadConfiguration("nlog.config");
        //     return serviceProvider;
        // }        



    }

}

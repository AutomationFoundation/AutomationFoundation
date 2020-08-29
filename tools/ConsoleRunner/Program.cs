using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using AutomationFoundation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ConsoleRunner
{
    public static class Program
    {
        private static IRuntimeHostBuilder CreateRuntimeHostBuilder() =>
            RuntimeHost.CreateDefaultBuilder()
                .ConfigureHostingEnvironment(environment =>
                {
                    environment.SetEnvironmentName("DEV");
                })
                .ConfigureAppConfiguration((environment, config) =>
                {
                    config.AddJsonFile("appSettings.json");
                    config.AddJsonFile($"appSettings.{environment.EnvironmentName}.json", true);
                })
                .ConfigureServices(services =>
                {
                    services.AddLogging(logging =>
                    {
                        logging.SetMinimumLevel(LogLevel.Information);
                        logging.AddNLog();
                    });

                    services.AddAutofac();
                })
                .UseStartup<Startup>();

        public static async Task Main()
        {
            try
            {
                using var host = CreateRuntimeHostBuilder().Build();
#if DEBUG
                await host.RunUntilCtrlCPressedAsync();
#else
                await host.RunUntilSigTermAsync();
#endif
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());

                Environment.ExitCode = -1;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using AutomationFoundation;
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
#if !DEBUG
                await host.RunUntilCtrlCPressedAsync();
#else
                #if NETFRAMEWORK
                    await host.RunUntilTaskKillAsync();
                #elif NETCOREAPP
                    await host.RunUntilSigTermAsync();
                #endif
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
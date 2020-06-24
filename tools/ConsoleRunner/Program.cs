using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using AutomationFoundation;
using AutomationFoundation.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ConsoleRunner
{
    public static class Program
    {
        private static IRuntimeHostBuilder CreateRuntimeHostBuilder()
        {
            return RuntimeHost.CreateDefaultBuilder()
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
                .UseRunStrategy<CtrlCRuntimeHostRunAsyncStrategy>()
                .UseStartup<Startup>();
        }

        public static async Task Main()
        {
            try
            {
                using var host = CreateRuntimeHostBuilder().Build();
                await host.RunAsync(shutdownTimeoutMs: 30000);
            }
            catch (Exception)
            {
                Environment.ExitCode = -1;
            }
        }
    }
}
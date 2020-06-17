using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using AutomationFoundation.Hosting;
using AutomationFoundation.Hosting.Abstractions.Builders;

namespace ConsoleRunner
{
    public static class Program
    {
        private static IRuntimeHostBuilder CreateRuntimeHostBuilder()
        {
            return RuntimeHost.CreateBuilder<DefaultRuntimeHostBuilder>()
                .ConfigureServices(services =>
                {
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
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
                Environment.ExitCode = -1;
            }
        }
    }
}
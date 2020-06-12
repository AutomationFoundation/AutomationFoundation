using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using AutomationFoundation.Hosting;
using AutomationFoundation.Hosting.Abstractions.Builders;

namespace ConsoleRunner
{
    public static class Program
    {
        private static readonly CancellationTokenSource CancellationSource = new CancellationTokenSource();

        private static IRuntimeHostBuilder CreateRuntimeHostBuilder()
        {
            return RuntimeHost.CreateBuilder<DefaultRuntimeHostBuilder>()
                .ConfigureServices(services =>
                {
                    services.AddAutofac();
                })
                .UseStartup<Startup>();
        }

        public static async Task Main()
        {
            Console.CancelKeyPress += async (sender, e) =>
            {
                await Console.Out.WriteLineAsync("Stopping...");

                CancellationSource.Cancel();
                e.Cancel = true; // Termination will occur when the host stops running.
            };

            await Console.Out.WriteLineAsync("Press CTRL+C to stop the application...");

            try
            {
                using var host = CreateRuntimeHostBuilder().Build();
                await host.RunAsync(CancellationSource.Token, shutdownTimeoutMs: 30000);
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
                Environment.ExitCode = -1;
            }

            await Console.Out.WriteLineAsync("Stopped.");
        }
    }
}
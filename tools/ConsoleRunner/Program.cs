using System;
using Autofac.Extensions.DependencyInjection;
using AutomationFoundation;
using AutomationFoundation.Hosting;
using AutomationFoundation.Hosting.Abstractions;
using AutomationFoundation.Hosting.Abstractions.Builder;
using ConsoleRunner.Abstractions;
using ConsoleRunner.Infrastructure.IO;

namespace ConsoleRunner
{
    public class Program
    {
        private readonly IConsoleWriter output = new ConsoleWriter();
        private readonly IConsoleReader input = new ConsoleReader();
        private readonly IRuntimeHost host;

        private Program(IRuntimeHost host)
        {
            this.host = host ?? throw new ArgumentNullException(nameof(host));
        }

        private static IRuntimeHostBuilder CreateRuntimeHostBuilder()
        {
            return RuntimeHost.CreateBuilder<DefaultRuntimeHostBuilder>()
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>();
        }

        public static void Main()
        {
            new Program(CreateRuntimeHostBuilder().Build())
                .Run();
        }

        private void Run()
        {
            try
            {
                host.Start();

                output.WriteLine("Press any key to stop...");
                input.WaitForAnyKey();

                host.Stop();
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.ToString(), ConsoleColor.Red);
            }

            output.WriteLine("Press any key to terminate...");
            input.WaitForAnyKey();
        }
    }
}
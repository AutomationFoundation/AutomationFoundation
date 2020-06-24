using System;
using AutomationFoundation.Hosting;
using AutomationFoundation.Runtime.Builders;
using ConsoleRunner.Abstractions;
using ConsoleRunner.Abstractions.DataAccess;
using ConsoleRunner.Infrastructure.WorkProcessors;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRunner
{
    internal partial class Startup : IStartup
    {
        public void ConfigureProcessors(IRuntimeBuilder runtimeBuilder, IHostingEnvironment environment)
        {
            if (runtimeBuilder == null)
            {
                throw new ArgumentNullException(nameof(runtimeBuilder));
            }

            using var scope = runtimeBuilder.ApplicationServices.CreateScope();
            ConfigureProcessors(runtimeBuilder, scope.ServiceProvider);
        }

        private static void ConfigureProcessors(IRuntimeBuilder runtimeBuilder, IServiceProvider serviceProvider)
        {
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            var configs = unitOfWork.AppProcessors.GetProcessorsForMachine(Environment.MachineName);

            foreach (var config in configs)
            {
                var builder = GetBuilder(config.ProcessorType);
                if (builder == null)
                {
                    throw new InvalidOperationException("The processor builder was not created.");
                }

                var processor = builder.Build(runtimeBuilder, config);
                if (processor == null)
                {
                    throw new InvalidOperationException("The processor was not built.");
                }

                runtimeBuilder.RegisterProcessor(processor);
            }
        }

        private static IApplicationProcessorBuilder GetBuilder(ProcessorType processorType)
        {
            switch (processorType)
            {
                case ProcessorType.Test:
                    return new TestProcessorBuilder();

                case ProcessorType.ScheduledJob:
                case ProcessorType.Task:
                    throw new NotSupportedException($"The processor type '{processorType}' is not supported.");
            }

            return null;
        }
    }
}
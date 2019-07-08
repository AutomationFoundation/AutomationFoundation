using System;
using AutomationFoundation.Features.ProducerConsumer;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Configuration;
using AutomationFoundation.Features.ProducerConsumer.Engines;
using AutomationFoundation.Hosting.Abstractions.Builder;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using AutomationFoundation.Runtime.Threading;
using ConsoleRunner.Abstractions;
using ConsoleRunner.Infrastructure.Diagnostics;
using ConsoleRunner.Infrastructure.IO;
using ConsoleRunner.Infrastructure.WorkProcessors;
using ConsoleRunner.Model;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRunner.Infrastructure.Builders
{
    public class ProducerConsumerProcessorBuilder : IProcessorBuilder
    {
        public Processor Build(IRuntimeBuilder runtimeBuilder, AppProcessor config)
        {
            if (runtimeBuilder == null)
            {
                throw new ArgumentNullException(nameof(runtimeBuilder));
            }
            else if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var producerEngine1 = BuildProducerEngine(runtimeBuilder, config, null);
            var producerEngine2 = BuildProducerEngine(runtimeBuilder, config, null);

            var consumerEngine = BuildConsumerEngine(runtimeBuilder, config);

            return new ProducerConsumerProcessor(
                config.Name,
                new[] { producerEngine1 },
                consumerEngine);
        }

        private IConsumerEngine BuildConsumerEngine(IRuntimeBuilder runtimeBuilder, AppProcessor config)
        {
            //return new AsynchronousConsumerEngine(
            //    WorkerPool.Create(),
            //    new ConsumerRunner<int>(
            //        new IntConsumer()),
            //    new StrategyErrorHandler(
            //        new LogToConsoleErrorStrategy(new ConsoleWriter(), LoggingLevel.All)));

            return new SynchronousConsumerEngine(
                WorkerPool.Create(),
                new ConsumerRunner<int>(
                    new IntConsumer()));
        }

        private IProducerEngine BuildProducerEngine(IRuntimeBuilder runtimeBuilder, AppProcessor config, ISynchronizationPolicy synchronizationPolicy)
        {
            return new ScheduledProducerEngine(
                new ProducerRunner<int>(
                    runtimeBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>(),
                    new RandomIntProducer(),
                    synchronizationPolicy,
                    false),
                new StrategyErrorHandler(
                    new LogToConsoleErrorStrategy(new ConsoleWriter(), LoggingLevel.All)),
                new PollingScheduler(TimeSpan.FromSeconds(5)),
                new ScheduledEngineOptions
                {
                    ContinueUntilEmpty = true
                });
        }
    }
}
using System;
using AutomationFoundation.Features.ProducerConsumer;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Configuration;
using AutomationFoundation.Features.ProducerConsumer.Engines;
using AutomationFoundation.Features.ProducerConsumer.Factories;
using AutomationFoundation.Features.ProducerConsumer.Strategies;
using AutomationFoundation.Hosting.Abstractions.Builder;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using AutomationFoundation.Runtime.Threading;
using AutomationFoundation.Runtime.Threading.Primitives;
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

            var producerEngine1 = BuildProducerEngine(runtimeBuilder, null);
            var producerEngine2 = BuildProducerEngine(runtimeBuilder, null);

            var consumerEngine = BuildConsumerEngine();

            return new ProducerConsumerProcessor<int>(
                config.Name,
                new CancellationSourceFactory(),
                new[] { producerEngine1, producerEngine2 },
                consumerEngine);
        }

        private IConsumerEngine<int> BuildConsumerEngine()
        {
            //return new AsynchronousConsumerEngine(
            //    WorkerPool.Create(),
            //    new ConsumerRunner<int>(
            //        new IntConsumer()),
            //    new StrategyErrorHandler(
            //        new LogToConsoleErrorStrategy(new ConsoleWriter(), LoggingLevel.All)));

            return new SynchronousConsumerEngine<int>(
                WorkerPool.Create(),
                new DefaultConsumerExecutionStrategy<int>(
                    new DefaultConsumerFactory<IntConsumer, int>()));
        }

        private IProducerEngine<int> BuildProducerEngine(IRuntimeBuilder runtimeBuilder, ISynchronizationPolicy synchronizationPolicy)
        {
            return new ScheduledProducerEngine<int>(
                new DefaultProducerExecutionStrategy<int>(
                    runtimeBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>(),
                    new DefaultProducerFactory<RandomIntProducer, int>(),
                    synchronizationPolicy,
                    false),
                new CancellationSourceFactory(),
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
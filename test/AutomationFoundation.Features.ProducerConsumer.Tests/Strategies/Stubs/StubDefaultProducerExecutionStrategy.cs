using System;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Strategies;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Tests.Strategies.Stubs
{
    public class StubDefaultProducerExecutionStrategy<TItem> : DefaultProducerExecutionStrategy<TItem>
    {
        public StubDefaultProducerExecutionStrategy(IServiceScopeFactory scopeFactory, Func<IServiceScope, IProducer<TItem>> producerFactory, ISynchronizationPolicy synchronizationPolicy, bool alwaysExecuteOnDefaultValue) : base(
            scopeFactory, producerFactory, synchronizationPolicy, alwaysExecuteOnDefaultValue)
        {
        }

        protected override Task AcquireSynchronizationLockAsync(ProducerConsumerContext<TItem> context)
        {
            return base.AcquireSynchronizationLockAsync(context);
        }

        protected override Task CreateProducerAsync(ProducerConsumerContext<TItem> context)
        {
            return base.CreateProducerAsync(context);
        }

        protected override IServiceScope CreateChildScope()
        {
            return base.CreateChildScope();
        }

        protected override Task ProduceAsync(ProducerConsumerContext<TItem> context)
        {
            return base.ProduceAsync(context);
        }

        protected override Guid GenerateIdentifier(IServiceScope scope)
        {
            return base.GenerateIdentifier(scope);
        }
    }
}
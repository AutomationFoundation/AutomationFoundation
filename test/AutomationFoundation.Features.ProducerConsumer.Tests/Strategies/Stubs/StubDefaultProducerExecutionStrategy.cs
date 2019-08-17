using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Strategies;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Tests.Strategies.Stubs
{
    public class StubDefaultProducerExecutionStrategy<TItem> : DefaultProducerExecutionStrategy<TItem>
    {
        public StubDefaultProducerExecutionStrategy(IServiceScopeFactory scopeFactory, IProducerFactory<TItem> producerFactory, ISynchronizationPolicy synchronizationPolicy, bool alwaysExecuteOnDefaultValue) : base(
            scopeFactory, producerFactory, synchronizationPolicy, alwaysExecuteOnDefaultValue)
        {
        }
    }
}
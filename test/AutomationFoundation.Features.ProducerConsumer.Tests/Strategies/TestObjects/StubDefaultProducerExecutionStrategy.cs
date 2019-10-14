using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Strategies.TestObjects
{
    public class StubDefaultProducerExecutionStrategy<TItem> : DefaultProducerExecutionStrategy<TItem>
    {
        public StubDefaultProducerExecutionStrategy(IServiceScopeFactory scopeFactory, IProducerResolver<TItem> producerFactory, ISynchronizationPolicy synchronizationPolicy, bool alwaysExecuteOnDefaultValue) : base(
            scopeFactory, producerFactory, synchronizationPolicy, alwaysExecuteOnDefaultValue)
        {
        }
    }
}
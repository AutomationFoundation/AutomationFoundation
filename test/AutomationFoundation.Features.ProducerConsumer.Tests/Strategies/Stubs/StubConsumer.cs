using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer.Tests.Strategies.Stubs
{
    public class StubConsumer<TItem> : IConsumer<TItem>
    {
        public Task ConsumeAsync(IProducerConsumerContext<TItem> context)
        {
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace ConsoleRunner.Infrastructure.WorkProcessors
{
    public class IntConsumer : IConsumer<int>
    {
        private static readonly Monitor Monitor = new Monitor("Consumed");

        static IntConsumer()
        {
            Monitor.Start();
        }

        public Task ConsumeAsync(IProducerConsumerContext<int> context)
        {
            Monitor.Increment();
            
            return Task.CompletedTask;
        }
    }
}
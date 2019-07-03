using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using ConsoleRunner.Infrastructure.IO;

namespace ConsoleRunner.Infrastructure.WorkProcessors
{
    public class IntConsumer : IConsumer<int>
    {
        private readonly Monitor monitor = new Monitor("Consumed", new ConsoleWriter());

        public IntConsumer()
        {
            monitor.Start();
        }

        public Task Consume(int item, CancellationToken cancellationToken)
        {
            return Task.Run(() => OnConsume(item, cancellationToken), cancellationToken);
        }

        private void OnConsume(int item, CancellationToken cancellationToken)
        {
            monitor.Increment();
        }
    }
}
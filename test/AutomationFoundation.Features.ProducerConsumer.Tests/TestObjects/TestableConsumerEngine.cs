using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer.TestObjects
{
    public class TestableConsumerEngine : IConsumerEngine<int>, IStartable, IStoppable
    {
        public virtual void Initialize(CancellationToken cancellationToken)
        {
        }

        public virtual void Consume(IProducerConsumerContext<int> context)
        {
        }

        public virtual Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task StopAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task WaitForCompletionAsync()
        {
            return Task.CompletedTask;
        }
    }
}
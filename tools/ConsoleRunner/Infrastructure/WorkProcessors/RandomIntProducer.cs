using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace ConsoleRunner.Infrastructure.WorkProcessors;

public class RandomIntProducer : IProducer<int>
{
    public Task<int> ProduceAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(1);
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions.Stubs
{
    public class StubProducerEngine : ProducerEngine<object>
    {
        public StubProducerEngine(ICancellationSourceFactory cancellationSourceFactory)
            : base(cancellationSourceFactory)
        {
        }

        protected override Task RunAsync(Action<IProducerConsumerContext<object>> onProducedCallback, CancellationToken cancellationToken, CancellationToken parentToken)
        {
            return Task.CompletedTask;
        }
    }
}
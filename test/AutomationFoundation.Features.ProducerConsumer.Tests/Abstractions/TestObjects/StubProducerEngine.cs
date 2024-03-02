using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

/* Unmerged change from project 'AutomationFoundation.Features.ProducerConsumer.Tests(net472)'
Before:
namespace AutomationFoundation.Features.ProducerConsumer.Abstractions.TestObjects
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
After:
namespace AutomationFoundation.Features.ProducerConsumer.Abstractions.TestObjects;

public class StubProducerEngine : ProducerEngine<object>
{
    public StubProducerEngine(ICancellationSourceFactory cancellationSourceFactory)
        : base(cancellationSourceFactory)
    {
    }

    protected override Task RunAsync(Action<IProducerConsumerContext<object>> onProducedCallback, CancellationToken cancellationToken, CancellationToken parentToken)
    {
        return Task.CompletedTask;
*/

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions.TestObjects;

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
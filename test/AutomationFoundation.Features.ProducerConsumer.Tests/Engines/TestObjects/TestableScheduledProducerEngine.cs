using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Engines.Configuration;
using AutomationFoundation.Runtime.Abstractions;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Features.ProducerConsumer.Engines.TestObjects;

public class TestableScheduledProducerEngine : ScheduledProducerEngine<object>
{
    private Action delayCallback;

    public TestableScheduledProducerEngine(IProducerExecutionStrategy<object> executionStrategy, ICancellationSourceFactory cancellationSourceFactory, IErrorHandler errorHandler, IScheduler scheduler, ScheduledEngineOptions options)
        : base(executionStrategy, cancellationSourceFactory, errorHandler, scheduler, options)
    {
    }

    protected override Task DelayAsync(TimeSpan delay, CancellationToken cancellationToken)
    {
        delayCallback?.Invoke();

        return base.DelayAsync(delay, cancellationToken);
    }

    public void SetDelayCallback(Action callback)
    {
        delayCallback = callback ?? throw new ArgumentNullException(nameof(callback));
    }
}
using System;
using System.Threading;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Threading.Primitives;

[TestFixture]
public class CancellationSourceTests
{
    [Test]
    public void AllowMultipleDisposals()
    {
        var target = new CancellationSource();
        target.Dispose();

        Assert.DoesNotThrow(() => target.Dispose());
    }

    [Test]
    public void IndicatesCancellationWhenImmediate()
    {
        using var target = new CancellationSource();
        Assert.False(target.IsCancellationRequested);

        target.RequestImmediateCancellation();

        Assert.True(target.IsCancellationRequested);
    }

    [Test]
    public void IndicatesCancellationWhenAfterAPeriodOfTime()
    {
        using var target = new CancellationSource();
        Assert.False(target.IsCancellationRequested);

        target.RequestCancellationAfter(TimeSpan.FromSeconds(1));

        Thread.Sleep(TimeSpan.FromSeconds(2));

        Assert.True(target.IsCancellationRequested);
    }
}
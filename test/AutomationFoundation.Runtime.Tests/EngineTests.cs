using System;
using AutomationFoundation.Runtime.TestObjects;
using NUnit.Framework;

namespace AutomationFoundation.Runtime;

[TestFixture]
public class EngineTests
{
    [Test]
    public void ThrowAnExceptionAfterDispose()
    {
        var target = new StubEngine();
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.ThisShouldCauseAnExceptionAfterDispose());
    }

    [Test]
    public void ShouldNotThrowAnExceptionBeforeDispose()
    {
        var target = new StubEngine();
        Assert.DoesNotThrow(() => target.ThisShouldCauseAnExceptionAfterDispose());

        target.Dispose();
    }
}
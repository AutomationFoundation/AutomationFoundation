using System;
using AutomationFoundation.Runtime.TestObjects;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Runtime.Tests(net472)'
Before:
namespace AutomationFoundation.Runtime
{
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
After:
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
*/

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
using System;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using Moq;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Runtime.Tests(net472)'
Before:
namespace AutomationFoundation.Runtime.Threading.Internal
{
    [TestFixture]
    public class WorkerCacheMonitorExtensionsTests
    {
        [Test]
        public void DoesNotThrowAnExceptionWhenTheValueIsNotDisposable()
        {
            var cacheMonitor = new Mock<IWorkerCacheMonitor>();

            Assert.DoesNotThrow(() => cacheMonitor.Object.DisposeIfNecessary());
        }

        [Test]
        public void DisposesTheMonitorWhenDisposable()
        {
            var cacheMonitor = new Mock<IWorkerCacheMonitor>();
            var disposable = cacheMonitor.As<IDisposable>();

            cacheMonitor.Object.DisposeIfNecessary();

            disposable.Verify(o => o.Dispose(), Times.Once);
        }
After:
namespace AutomationFoundation.Runtime.Threading.Internal;

[TestFixture]
public class WorkerCacheMonitorExtensionsTests
{
    [Test]
    public void DoesNotThrowAnExceptionWhenTheValueIsNotDisposable()
    {
        var cacheMonitor = new Mock<IWorkerCacheMonitor>();

        Assert.DoesNotThrow(() => cacheMonitor.Object.DisposeIfNecessary());
    }

    [Test]
    public void DisposesTheMonitorWhenDisposable()
    {
        var cacheMonitor = new Mock<IWorkerCacheMonitor>();
        var disposable = cacheMonitor.As<IDisposable>();

        cacheMonitor.Object.DisposeIfNecessary();

        disposable.Verify(o => o.Dispose(), Times.Once);
*/

namespace AutomationFoundation.Runtime.Threading.Internal;

[TestFixture]
public class WorkerCacheMonitorExtensionsTests
{
    [Test]
    public void DoesNotThrowAnExceptionWhenTheValueIsNotDisposable()
    {
        var cacheMonitor = new Mock<IWorkerCacheMonitor>();

        Assert.DoesNotThrow(() => cacheMonitor.Object.DisposeIfNecessary());
    }

    [Test]
    public void DisposesTheMonitorWhenDisposable()
    {
        var cacheMonitor = new Mock<IWorkerCacheMonitor>();
        var disposable = cacheMonitor.As<IDisposable>();

        cacheMonitor.Object.DisposeIfNecessary();

        disposable.Verify(o => o.Dispose(), Times.Once);
    }
}
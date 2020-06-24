using System;
using Moq;
using NUnit.Framework;

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
    }
}
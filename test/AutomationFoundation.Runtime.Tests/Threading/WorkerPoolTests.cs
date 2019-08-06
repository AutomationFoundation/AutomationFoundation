using System;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using AutomationFoundation.Runtime.Tests.Stubs;
using AutomationFoundation.Runtime.Threading;
using AutomationFoundation.Runtime.Threading.Internal;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Tests.Threading
{
    [TestFixture]
    public class WorkerPoolTests
    {
        [Test]
        public void ThrowsAnExceptionWhenTheCacheIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (new WorkerPool(null, new Mock<IWorkerCacheMonitor>().Object))
                {
                    // This code block intentionally left blank.
                }
            });
        }

        [Test]
        public void ThrowsAnExceptionWhenTheCacheMonitorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (new WorkerPool(new Mock<IWorkerCache>().Object, null))
                {
                    // This code block intentionally left blank.
                }
            });
        }

        [Test]
        public void CreatesThePoolAsExpected()
        {
            using (var target = WorkerPool.Create())
            {
                Assert.IsNotNull(target);
            }
        }

        [Test]
        public void GetsTheWorkerAsExpected()
        {
            var worker = new Mock<IRuntimeWorker>();
            var cacheMonitor = new Mock<IWorkerCacheMonitor>();

            var cache = new Mock<IWorkerCache>();
            cache.Setup(o => o.Get()).Returns(worker.Object);

            using (var target = new WorkerPool(cache.Object, cacheMonitor.Object))
            {
                var result = target.Get(OnRun, OnCompleted);

                Assert.IsNotNull(result);
                Assert.IsInstanceOf<PooledWorker>(result);
            }
        }

        [Test]
        public void ThrowsAnExceptionWhenTheOnRunCallbackIsNull()
        {
            var cache = new Mock<IWorkerCache>();
            var cacheMonitor = new Mock<IWorkerCacheMonitor>();

            using (var target = new WorkerPool(cache.Object, cacheMonitor.Object))
            {
                Assert.Throws<ArgumentNullException>(() => target.Get(null, OnCompleted));
            }
        }

        [Test]
        public void DoesNotThrowAnExceptionWhenThePostCompletedCallbackIsNull()
        {
            var cache = new Mock<IWorkerCache>();
            var cacheMonitor = new Mock<IWorkerCacheMonitor>();

            using (var target = new WorkerPool(cache.Object, cacheMonitor.Object))
            {
                Assert.DoesNotThrow(() => target.Get(OnRun, null));
            }
        }

        [Test]
        public void ReleasesTheWorkerAsExpectedIfAnErrorOccurs()
        {
            var worker = new Mock<IRuntimeWorker>();
            var cacheMonitor = new Mock<IWorkerCacheMonitor>();

            var cache = new Mock<IWorkerCache>();
            cache.Setup(o => o.Get()).Returns(worker.Object);

            using (var target = new TestableWorkerPool(cache.Object, cacheMonitor.Object))
            {
                target.OnCreatePooledWorkerCallback = o => throw new ApplicationException("Test exception");

                Assert.Throws<ApplicationException>(() => target.Get(OnRun, OnCompleted));
                cache.Verify(o => o.Release(worker.Object), Times.Once);
            }
        }

        [Test]
        public void ThrowsAnExceptionWhenTheWorkerIsNull()
        {
            var worker = new Mock<IRuntimeWorker>();
            var cacheMonitor = new Mock<IWorkerCacheMonitor>();

            var cache = new Mock<IWorkerCache>();
            cache.Setup(o => o.Get()).Returns(worker.Object);

            using (var target = new TestableWorkerPool(cache.Object, cacheMonitor.Object))
            {
                target.OnBaseCreatePooledWorkerCallback = o => null;

                Assert.Throws<ArgumentNullException>(() => target.Get(OnRun, OnCompleted));
                cache.Verify(o => o.Release(worker.Object), Times.Once);
            }
        }

        private static void OnRun()
        {
            // This method intentionally left blank.
        }

        private static void OnCompleted()
        {
            // This method intentionally left blank.
        }
    }
}

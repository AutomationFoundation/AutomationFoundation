using System;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using AutomationFoundation.Runtime.Threading.Internal;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Tests.Threading.Internal
{
    [TestFixture]
    public class PooledWorkerTests
    {
        [Test]
        public void ThrowsAnExceptionWhenTheCacheIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (new PooledWorker(null, new Mock<IRuntimeWorker>().Object))
                {
                    // This code block intentionally left blank.
                }
            });
        }

        [Test]
        public void ThrowsAnExceptionWhenTheWorkerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (new PooledWorker(new Mock<IWorkerCache>().Object, null))
                {
                    // This code block intentionally left blank.
                }
            });
        }

        [Test]
        public void RunTheWorkerAsynchronouslyAsExpected()
        {
            var cache = new Mock<IWorkerCache>();
            var worker = new Mock<IRuntimeWorker>();

            using (var target = new PooledWorker(cache.Object, worker.Object))
            {
                target.RunAsync();

                worker.Verify(o => o.RunAsync(), Times.Once);
            }
        }

        [Test]
        public void RunTheWorkerSynchronouslyAsExpected()
        {
            var cache = new Mock<IWorkerCache>();
            var worker = new Mock<IRuntimeWorker>();

            using (var target = new PooledWorker(cache.Object, worker.Object))
            {
                target.Run();

                worker.Verify(o => o.Run(), Times.Once);
            }
        }

        [Test]
        public void ReleasesTheWorkerToThePoolOnDispose()
        {
            var cache = new Mock<IWorkerCache>();
            var worker = new Mock<IRuntimeWorker>();

            using (new PooledWorker(cache.Object, worker.Object))
            {
                // This code block intentionally left blank.
            }

            cache.Verify(o => o.Release(worker.Object), Times.Once);
        }

        [Test]
        public void WaitsForTheWorkerToComplete()
        {
            var cache = new Mock<IWorkerCache>();
            var worker = new Mock<IRuntimeWorker>();

            using (var target = new PooledWorker(cache.Object, worker.Object))
            {
                target.WaitForCompletion();
            }

            worker.Verify(o => o.WaitForCompletion(), Times.Once);
        }

        [Test]
        public async Task WaitsForTheWorkerToCompleteAsynchronously()
        {
            var cache = new Mock<IWorkerCache>();

            var worker = new Mock<IRuntimeWorker>();
            worker.Setup(o => o.WaitForCompletionAsync()).Returns(Task.CompletedTask);

            using (var target = new PooledWorker(cache.Object, worker.Object))
            {
                await target.WaitForCompletionAsync();
            }

            worker.Verify(o => o.WaitForCompletionAsync(), Times.Once);
        }
    }
}
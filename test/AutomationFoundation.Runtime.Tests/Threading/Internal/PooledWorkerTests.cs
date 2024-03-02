using System;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using Moq;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Runtime.Tests(net472)'
Before:
namespace AutomationFoundation.Runtime.Threading.Internal
{
    [TestFixture]
    public class PooledWorkerTests
    {
        private Mock<IWorkerCache> cache;
        private Mock<IRuntimeWorker> worker;

        [SetUp]
        public void Setup()
        {
            cache = new Mock<IWorkerCache>();
            worker = new Mock<IRuntimeWorker>();
        }

        [Test]
        public void ThrowsAnExceptionWhenTheCacheIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (new PooledWorker(null, worker.Object))
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
                using (new PooledWorker(cache.Object, null))
                {
                    // This code block intentionally left blank.
                }
            });
        }

        [Test]
        public void RunTheWorkerAsynchronouslyAsExpected()
        {
            using var target = new PooledWorker(cache.Object, worker.Object);
            target.RunAsync();

            worker.Verify(o => o.RunAsync(), Times.Once);
        }

        [Test]
        public void RunTheWorkerSynchronouslyAsExpected()
        {
            using var target = new PooledWorker(cache.Object, worker.Object);
            target.Run();

            worker.Verify(o => o.Run(), Times.Once);
        }

        [Test]
        public void ReleasesTheWorkerToThePoolOnDispose()
        {
            using (new PooledWorker(cache.Object, worker.Object))
            {
                // This code block intentionally left blank.
            }

            cache.Verify(o => o.Release(worker.Object), Times.Once);
        }

        [Test]
        public void WaitsForTheWorkerToComplete()
        {
            using var target = new PooledWorker(cache.Object, worker.Object);
            target.WaitForCompletion();

            worker.Verify(o => o.WaitForCompletion(), Times.Once);
        }

        [Test]
        public async Task WaitsForTheWorkerToCompleteAsynchronously()
        {
            worker.Setup(o => o.WaitForCompletionAsync()).Returns(Task.CompletedTask);

            using var target = new PooledWorker(cache.Object, worker.Object);
            await target.WaitForCompletionAsync();

            worker.Verify(o => o.WaitForCompletionAsync(), Times.Once);
        }

        [Test]
        public void ThrowsAnExceptionWhenAfterDispose()
        {
            var target = new PooledWorker(cache.Object, worker.Object);
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Run());
        }
After:
namespace AutomationFoundation.Runtime.Threading.Internal;

[TestFixture]
public class PooledWorkerTests
{
    private Mock<IWorkerCache> cache;
    private Mock<IRuntimeWorker> worker;

    [SetUp]
    public void Setup()
    {
        cache = new Mock<IWorkerCache>();
        worker = new Mock<IRuntimeWorker>();
    }

    [Test]
    public void ThrowsAnExceptionWhenTheCacheIsNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            using (new PooledWorker(null, worker.Object))
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
            using (new PooledWorker(cache.Object, null))
            {
                // This code block intentionally left blank.
            }
        });
    }

    [Test]
    public void RunTheWorkerAsynchronouslyAsExpected()
    {
        using var target = new PooledWorker(cache.Object, worker.Object);
        target.RunAsync();

        worker.Verify(o => o.RunAsync(), Times.Once);
    }

    [Test]
    public void RunTheWorkerSynchronouslyAsExpected()
    {
        using var target = new PooledWorker(cache.Object, worker.Object);
        target.Run();

        worker.Verify(o => o.Run(), Times.Once);
    }

    [Test]
    public void ReleasesTheWorkerToThePoolOnDispose()
    {
        using (new PooledWorker(cache.Object, worker.Object))
        {
            // This code block intentionally left blank.
        }

        cache.Verify(o => o.Release(worker.Object), Times.Once);
    }

    [Test]
    public void WaitsForTheWorkerToComplete()
    {
        using var target = new PooledWorker(cache.Object, worker.Object);
        target.WaitForCompletion();

        worker.Verify(o => o.WaitForCompletion(), Times.Once);
    }

    [Test]
    public async Task WaitsForTheWorkerToCompleteAsynchronously()
    {
        worker.Setup(o => o.WaitForCompletionAsync()).Returns(Task.CompletedTask);

        using var target = new PooledWorker(cache.Object, worker.Object);
        await target.WaitForCompletionAsync();

        worker.Verify(o => o.WaitForCompletionAsync(), Times.Once);
    }

    [Test]
    public void ThrowsAnExceptionWhenAfterDispose()
    {
        var target = new PooledWorker(cache.Object, worker.Object);
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.Run());
*/

namespace AutomationFoundation.Runtime.Threading.Internal;

[TestFixture]
public class PooledWorkerTests
{
    private Mock<IWorkerCache> cache;
    private Mock<IRuntimeWorker> worker;

    [SetUp]
    public void Setup()
    {
        cache = new Mock<IWorkerCache>();
        worker = new Mock<IRuntimeWorker>();
    }

    [Test]
    public void ThrowsAnExceptionWhenTheCacheIsNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            using (new PooledWorker(null, worker.Object))
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
            using (new PooledWorker(cache.Object, null))
            {
                // This code block intentionally left blank.
            }
        });
    }

    [Test]
    public void RunTheWorkerAsynchronouslyAsExpected()
    {
        using var target = new PooledWorker(cache.Object, worker.Object);
        target.RunAsync();

        worker.Verify(o => o.RunAsync(), Times.Once);
    }

    [Test]
    public void RunTheWorkerSynchronouslyAsExpected()
    {
        using var target = new PooledWorker(cache.Object, worker.Object);
        target.Run();

        worker.Verify(o => o.Run(), Times.Once);
    }

    [Test]
    public void ReleasesTheWorkerToThePoolOnDispose()
    {
        using (new PooledWorker(cache.Object, worker.Object))
        {
            // This code block intentionally left blank.
        }

        cache.Verify(o => o.Release(worker.Object), Times.Once);
    }

    [Test]
    public void WaitsForTheWorkerToComplete()
    {
        using var target = new PooledWorker(cache.Object, worker.Object);
        target.WaitForCompletion();

        worker.Verify(o => o.WaitForCompletion(), Times.Once);
    }

    [Test]
    public async Task WaitsForTheWorkerToCompleteAsynchronously()
    {
        worker.Setup(o => o.WaitForCompletionAsync()).Returns(Task.CompletedTask);

        using var target = new PooledWorker(cache.Object, worker.Object);
        await target.WaitForCompletionAsync();

        worker.Verify(o => o.WaitForCompletionAsync(), Times.Once);
    }

    [Test]
    public void ThrowsAnExceptionWhenAfterDispose()
    {
        var target = new PooledWorker(cache.Object, worker.Object);
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.Run());
    }
}
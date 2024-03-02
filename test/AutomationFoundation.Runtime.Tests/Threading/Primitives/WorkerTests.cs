using System;
using System.Threading.Tasks;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Runtime.Tests(net472)'
Before:
namespace AutomationFoundation.Runtime.Threading.Primitives
{
    [TestFixture]
    public class WorkerTests
    {
        [Test]
        public void CleanUpTheWorkerAsExpectedBeforeInitialize()
        {
            using var target = new Worker();
            Assert.False(target.Initialized);

            target.Reset();

            Assert.False(target.Initialized);
        }

        [Test]
        public void ThrowsAnExceptionIfInitializedWhileAlreadyInitialized()
        {
            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext());

            Assert.True(target.Initialized);
            Assert.Throws<InvalidOperationException>(() => target.Initialize(new WorkerExecutionContext()));
        }

        [Test]
        public void WorkerMustSupportReuse()
        {
            bool called1 = false, called2 = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                OnRunCallback = () => called1 = true
            });

            target.Run();
            target.Reset();

            target.Initialize(new WorkerExecutionContext
            {
                OnRunCallback = () => called2 = true
            });

            target.Run();

            Assert.True(called1);
            Assert.True(called2);
        }

        [Test]
        public void ThrowsAnExceptionIfInitializedWhileRunning()
        {
            var called = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                OnRunCallback = () =>
                {
                    // ReSharper disable once AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => target.Initialize(new WorkerExecutionContext()));
                    called = true;
                }
            });

            target.Run();

            Assert.True(called);
        }

        [Test]
        public void ThrowsAnExceptionIfInitializedAfterDisposed()
        {
            var target = new Worker();
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Initialize(new WorkerExecutionContext()));
        }

        [Test]
        public void ThrowsAnExceptionIfRunAsyncCalledBeforeInitialize()
        {
            using var target = new Worker();
            Assert.Throws<InvalidOperationException>(() => target.RunAsync());
        }

        [Test]
        public void ThrowsAnExceptionIfRunSynchronouslyCalledBeforeInitialize()
        {
            using var target = new Worker();
            Assert.Throws<InvalidOperationException>(() => target.Run());
        }

        [Test]
        public void ReturnsTrueWhenRunning()
        {
            var called = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                OnRunCallback = () =>
                {
                    Assert.True(target.IsRunning);
                    called = true;
                }
            });

            target.Run();

            Assert.True(called);
        }

        [Test]
        public void ReturnsFalseWhenNotRunning()
        {
            using var target = new Worker();
            Assert.False(target.IsRunning);
        }

        [Test]
        public async Task RunsTheTaskAsynchronously()
        {
            var called = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                OnRunCallback = () => called = true
            });

            await target.RunAsync();

            Assert.True(called);
        }

        [Test]
        public void ThrowsAnExceptionWhenRunAsyncCalledAfterDispose()
        {
            var target = new Worker();
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.RunAsync());
        }

        [Test]
        public void ThrowsAnExceptionWhenRunSynchronouslyCalledAfterDispose()
        {
            var target = new Worker();
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Run());
        }

        [Test]
        public void RaisesTheErrorCallbackWhenAnExceptionOccursWhileRunning()
        {
            var called = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                OnRunCallback = () =>
                {
                    called = true;
                    throw new ApplicationException("Something broke.");
                }
            });

            var aggregateException = Assert.Throws<AggregateException>(() => target.Run());
            CollectionAssert.AllItemsAreInstancesOfType(aggregateException.InnerExceptions, typeof(ApplicationException));

            Assert.True(called);
        }

        [Test]
        public void RaisesTheErrorCallbackWhenAnExceptionOccursWhileExecutingThePostCompletedCallback()
        {
            var called = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                PostCompletedCallback = () =>
                {
                    called = true;
                    throw new ApplicationException("Something broke.");
                }
            });

            var aggregateException = Assert.Throws<AggregateException>(() => target.Run());
            CollectionAssert.AllItemsAreInstancesOfType(aggregateException.InnerExceptions, typeof(ApplicationException));

            Assert.True(called);
        }

        [Test]
        public void RunsThePostCompletedCallbackAsExpected()
        {
            var called = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                PostCompletedCallback = () => { called = true; }
            });

            target.Run();

            Assert.True(called);
        }

        [Test]
        public void RunsTheActionsInTheCorrectOrderSynchronouslyAsExpected()
        {
            var called1 = false;
            var called2 = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                OnRunCallback = () =>
                {
                    Assert.False(called1);
                    Assert.False(called2);

                    called1 = true;
                },
                PostCompletedCallback = () =>
                {
                    Assert.True(called1);
                    Assert.False(called2);

                    called2 = true;
                }
            });

            target.Run();

            Assert.True(called1);
            Assert.True(called2);
        }

        [Test]
        public async Task RunsTheActionsInTheCorrectOrderAsynchronouslyAsExpected()
        {
            var called1 = false;
            var called2 = false;

            using var target = new Worker();
            target.Initialize(new WorkerExecutionContext
            {
                OnRunCallback = () =>
                {
                    Assert.False(called1);
                    Assert.False(called2);

                    called1 = true;
                },
                PostCompletedCallback = () =>
                {
                    Assert.True(called1);
                    Assert.False(called2);

                    called2 = true;
                }
            });

            await target.RunAsync();

            Assert.True(called1);
            Assert.True(called2);
        }
After:
namespace AutomationFoundation.Runtime.Threading.Primitives;

[TestFixture]
public class WorkerTests
{
    [Test]
    public void CleanUpTheWorkerAsExpectedBeforeInitialize()
    {
        using var target = new Worker();
        Assert.False(target.Initialized);

        target.Reset();

        Assert.False(target.Initialized);
    }

    [Test]
    public void ThrowsAnExceptionIfInitializedWhileAlreadyInitialized()
    {
        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext());

        Assert.True(target.Initialized);
        Assert.Throws<InvalidOperationException>(() => target.Initialize(new WorkerExecutionContext()));
    }

    [Test]
    public void WorkerMustSupportReuse()
    {
        bool called1 = false, called2 = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () => called1 = true
        });

        target.Run();
        target.Reset();

        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () => called2 = true
        });

        target.Run();

        Assert.True(called1);
        Assert.True(called2);
    }

    [Test]
    public void ThrowsAnExceptionIfInitializedWhileRunning()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.Throws<InvalidOperationException>(() => target.Initialize(new WorkerExecutionContext()));
                called = true;
            }
        });

        target.Run();

        Assert.True(called);
    }

    [Test]
    public void ThrowsAnExceptionIfInitializedAfterDisposed()
    {
        var target = new Worker();
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.Initialize(new WorkerExecutionContext()));
    }

    [Test]
    public void ThrowsAnExceptionIfRunAsyncCalledBeforeInitialize()
    {
        using var target = new Worker();
        Assert.Throws<InvalidOperationException>(() => target.RunAsync());
    }

    [Test]
    public void ThrowsAnExceptionIfRunSynchronouslyCalledBeforeInitialize()
    {
        using var target = new Worker();
        Assert.Throws<InvalidOperationException>(() => target.Run());
    }

    [Test]
    public void ReturnsTrueWhenRunning()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                Assert.True(target.IsRunning);
                called = true;
            }
        });

        target.Run();

        Assert.True(called);
    }

    [Test]
    public void ReturnsFalseWhenNotRunning()
    {
        using var target = new Worker();
        Assert.False(target.IsRunning);
    }

    [Test]
    public async Task RunsTheTaskAsynchronously()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () => called = true
        });

        await target.RunAsync();

        Assert.True(called);
    }

    [Test]
    public void ThrowsAnExceptionWhenRunAsyncCalledAfterDispose()
    {
        var target = new Worker();
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.RunAsync());
    }

    [Test]
    public void ThrowsAnExceptionWhenRunSynchronouslyCalledAfterDispose()
    {
        var target = new Worker();
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.Run());
    }

    [Test]
    public void RaisesTheErrorCallbackWhenAnExceptionOccursWhileRunning()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                called = true;
                throw new ApplicationException("Something broke.");
            }
        });

        var aggregateException = Assert.Throws<AggregateException>(() => target.Run());
        CollectionAssert.AllItemsAreInstancesOfType(aggregateException.InnerExceptions, typeof(ApplicationException));

        Assert.True(called);
    }

    [Test]
    public void RaisesTheErrorCallbackWhenAnExceptionOccursWhileExecutingThePostCompletedCallback()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            PostCompletedCallback = () =>
            {
                called = true;
                throw new ApplicationException("Something broke.");
            }
        });

        var aggregateException = Assert.Throws<AggregateException>(() => target.Run());
        CollectionAssert.AllItemsAreInstancesOfType(aggregateException.InnerExceptions, typeof(ApplicationException));

        Assert.True(called);
    }

    [Test]
    public void RunsThePostCompletedCallbackAsExpected()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            PostCompletedCallback = () => { called = true; }
        });

        target.Run();

        Assert.True(called);
    }

    [Test]
    public void RunsTheActionsInTheCorrectOrderSynchronouslyAsExpected()
    {
        var called1 = false;
        var called2 = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                Assert.False(called1);
                Assert.False(called2);

                called1 = true;
            },
            PostCompletedCallback = () =>
            {
                Assert.True(called1);
                Assert.False(called2);

                called2 = true;
            }
        });

        target.Run();

        Assert.True(called1);
        Assert.True(called2);
    }

    [Test]
    public async Task RunsTheActionsInTheCorrectOrderAsynchronouslyAsExpected()
    {
        var called1 = false;
        var called2 = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                Assert.False(called1);
                Assert.False(called2);

                called1 = true;
            },
            PostCompletedCallback = () =>
            {
                Assert.True(called1);
                Assert.False(called2);

                called2 = true;
            }
        });

        await target.RunAsync();

        Assert.True(called1);
        Assert.True(called2);
*/

namespace AutomationFoundation.Runtime.Threading.Primitives;

[TestFixture]
public class WorkerTests
{
    [Test]
    public void CleanUpTheWorkerAsExpectedBeforeInitialize()
    {
        using var target = new Worker();
        Assert.False(target.Initialized);

        target.Reset();

        Assert.False(target.Initialized);
    }

    [Test]
    public void ThrowsAnExceptionIfInitializedWhileAlreadyInitialized()
    {
        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext());

        Assert.True(target.Initialized);
        Assert.Throws<InvalidOperationException>(() => target.Initialize(new WorkerExecutionContext()));
    }

    [Test]
    public void WorkerMustSupportReuse()
    {
        bool called1 = false, called2 = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () => called1 = true
        });

        target.Run();
        target.Reset();

        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () => called2 = true
        });

        target.Run();

        Assert.True(called1);
        Assert.True(called2);
    }

    [Test]
    public void ThrowsAnExceptionIfInitializedWhileRunning()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.Throws<InvalidOperationException>(() => target.Initialize(new WorkerExecutionContext()));
                called = true;
            }
        });

        target.Run();

        Assert.True(called);
    }

    [Test]
    public void ThrowsAnExceptionIfInitializedAfterDisposed()
    {
        var target = new Worker();
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.Initialize(new WorkerExecutionContext()));
    }

    [Test]
    public void ThrowsAnExceptionIfRunAsyncCalledBeforeInitialize()
    {
        using var target = new Worker();
        Assert.Throws<InvalidOperationException>(() => target.RunAsync());
    }

    [Test]
    public void ThrowsAnExceptionIfRunSynchronouslyCalledBeforeInitialize()
    {
        using var target = new Worker();
        Assert.Throws<InvalidOperationException>(() => target.Run());
    }

    [Test]
    public void ReturnsTrueWhenRunning()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                Assert.True(target.IsRunning);
                called = true;
            }
        });

        target.Run();

        Assert.True(called);
    }

    [Test]
    public void ReturnsFalseWhenNotRunning()
    {
        using var target = new Worker();
        Assert.False(target.IsRunning);
    }

    [Test]
    public async Task RunsTheTaskAsynchronously()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () => called = true
        });

        await target.RunAsync();

        Assert.True(called);
    }

    [Test]
    public void ThrowsAnExceptionWhenRunAsyncCalledAfterDispose()
    {
        var target = new Worker();
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.RunAsync());
    }

    [Test]
    public void ThrowsAnExceptionWhenRunSynchronouslyCalledAfterDispose()
    {
        var target = new Worker();
        target.Dispose();

        Assert.Throws<ObjectDisposedException>(() => target.Run());
    }

    [Test]
    public void RaisesTheErrorCallbackWhenAnExceptionOccursWhileRunning()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                called = true;
                throw new ApplicationException("Something broke.");
            }
        });

        var aggregateException = Assert.Throws<AggregateException>(() => target.Run());
        CollectionAssert.AllItemsAreInstancesOfType(aggregateException.InnerExceptions, typeof(ApplicationException));

        Assert.True(called);
    }

    [Test]
    public void RaisesTheErrorCallbackWhenAnExceptionOccursWhileExecutingThePostCompletedCallback()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            PostCompletedCallback = () =>
            {
                called = true;
                throw new ApplicationException("Something broke.");
            }
        });

        var aggregateException = Assert.Throws<AggregateException>(() => target.Run());
        CollectionAssert.AllItemsAreInstancesOfType(aggregateException.InnerExceptions, typeof(ApplicationException));

        Assert.True(called);
    }

    [Test]
    public void RunsThePostCompletedCallbackAsExpected()
    {
        var called = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            PostCompletedCallback = () => { called = true; }
        });

        target.Run();

        Assert.True(called);
    }

    [Test]
    public void RunsTheActionsInTheCorrectOrderSynchronouslyAsExpected()
    {
        var called1 = false;
        var called2 = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                Assert.False(called1);
                Assert.False(called2);

                called1 = true;
            },
            PostCompletedCallback = () =>
            {
                Assert.True(called1);
                Assert.False(called2);

                called2 = true;
            }
        });

        target.Run();

        Assert.True(called1);
        Assert.True(called2);
    }

    [Test]
    public async Task RunsTheActionsInTheCorrectOrderAsynchronouslyAsExpected()
    {
        var called1 = false;
        var called2 = false;

        using var target = new Worker();
        target.Initialize(new WorkerExecutionContext
        {
            OnRunCallback = () =>
            {
                Assert.False(called1);
                Assert.False(called2);

                called1 = true;
            },
            PostCompletedCallback = () =>
            {
                Assert.True(called1);
                Assert.False(called2);

                called2 = true;
            }
        });

        await target.RunAsync();

        Assert.True(called1);
        Assert.True(called2);
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Threading
{
    [TestFixture]
    public class TaskExtensionTests
    {
        [Test]
        public void ReturnsWithoutAbandoningAsExpected()
        {
            var task = Task.Run(() => Task.CompletedTask);
            Assert.DoesNotThrowAsync(async () => await task.AbandonWhen(CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheTokenIsSignaled()
        {
            var task = Task.Run(async () => await Task.Delay(Timeout.InfiniteTimeSpan));
            Assert.ThrowsAsync<TaskAbandonedException>(async () => await task.AbandonWhen(new CancellationToken(true)));
        }

        [Test]
        public void PropagatesTheExceptionAsExpected()
        {
            var task = Task.Run(() => throw new InvalidOperationException());
            Assert.ThrowsAsync<InvalidOperationException>(async () => await task.AbandonWhen(CancellationToken.None));
        }

        [Test]
        public async Task ReturnsResultWithoutAbandoningAsExpected()
        {
            var task = Task.Run(() => Task.FromResult(1));
            var result = await task.AbandonWhen(CancellationToken.None);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheTokenIsSignaled2()
        {
            var task = Task.Run(async () =>
            {
                await Task.Delay(Timeout.InfiniteTimeSpan);
                return 1;
            });

            Assert.ThrowsAsync<TaskAbandonedException>(async () => await task.AbandonWhen(new CancellationToken(true)));
        }

        [Test]
        public void PropagatesTheExceptionAsExpected2()
        {
            var task = Task.Run(() =>
            {
                if (true)
                {
                    throw new InvalidOperationException();
                }

#pragma warning disable CS0162
                return 1;
#pragma warning restore CS0162
            });

            Assert.ThrowsAsync<InvalidOperationException>(async () => await task.AbandonWhen(CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenTaskIsNull_IsRunning()
        {
            Assert.Throws<ArgumentNullException>(() => TaskExtensions.IsRunning(null));
        }

        [Test]
        public void ThrowsAnExceptionWhenTaskIsNull_DisposeIfNecessary()
        {
            Assert.Throws<ArgumentNullException>(() => TaskExtensions.DisposeIfNecessary(null));
        }

        [Test]
        public void DisposesOfTheTaskAsExpectedWhenFirstCreated()
        {
            var target = new Task(() => { }).ContinueWith(t1 => { });
            Assert.DoesNotThrow(() => target.DisposeIfNecessary());
        }

        [Test]
        public void ReturnFalseIfCompleted()
        {
            var target = Task.CompletedTask;

            Assert.False(target.IsRunning());
        }

        [Test]
        public void ReturnFalseIfFaulted()
        {
            var target = Task.FromException(new Exception());

            Assert.False(target.IsRunning());
        }

        [Test]
        public void ReturnFalseIfCancelled()
        {
            var token = new CancellationToken(true);            
            var target = Task.FromCanceled(token);

            Assert.False(target.IsRunning());
        }

        [Test]
        public void ReturnFalseCompletedWithResult()
        {
            var target = Task.FromResult(1);

            Assert.False(target.IsRunning());
        }

        [Test]
        public void ReturnFalseIfCancelledWithResult()
        {
            var token = new CancellationToken(true);
            var target = Task.FromCanceled<int>(token);

            Assert.False(target.IsRunning());
        }
    }
}
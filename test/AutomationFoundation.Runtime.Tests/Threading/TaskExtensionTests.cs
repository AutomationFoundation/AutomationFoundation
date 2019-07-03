using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Threading;
using NUnit.Framework;
using TaskExtensions = AutomationFoundation.Runtime.Threading.TaskExtensions;

namespace AutomationFoundation.Runtime.Tests.Threading
{
    [TestFixture]
    public class TaskExtensionTests
    {
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
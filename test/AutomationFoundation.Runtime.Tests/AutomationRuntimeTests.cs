using System;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Tests
{
#if DEBUG
    [TestFixture]
    public class AutomationRuntimeTests
    {
        [Test]
        public void ThrowAnExceptionWhenStartedAfterDisposed()
        {
            var target = new AutomationRuntime();
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Start());
        }

        [Test]
        public void ThrowAnExceptionWhenStoppedAfterDisposed()
        {
            var target = new AutomationRuntime();
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Stop());
        }
    }
#endif
}
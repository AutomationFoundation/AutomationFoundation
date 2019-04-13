using AutomationFoundation.Tests.Stubs;
using NUnit.Framework;

namespace AutomationFoundation.Tests
{
    [TestFixture]
    public class DisposableObjectTests
    {
        [Test]
        public void CallsTheCallbackAsExpected()
        {
            var called = false;

            var target = new StubDisposableObject(disposed => called = true);
            target.Dispose();

            Assert.True(called);
        }

        [Test]
        public void DoesNotThrowExceptionsWhenDisposedMultipleTimes()
        {
            var target = new StubDisposableObject();
            target.Dispose();

            Assert.DoesNotThrow(() => target.Dispose());
        }

        [Test]
        public void IdentifiesAsDisposed()
        {
            var target = new StubDisposableObject();
            target.Dispose();

            Assert.True(target.Disposed);
        }
    }
}
using System;
using NUnit.Framework;

namespace AutomationFoundation.Runtime
{
    [TestFixture]
    public class ErrorHandlingContextTests
    {
        [Test]
        public void ShouldRethrowTheExceptionTest()
        {
            var target = new ErrorHandlingContext(this, ErrorSeverityLevel.NonFatal, new Exception());

            Assert.Throws<Exception>(() => target.RethrowErrorIfNotHandled());
        }

        [Test]
        public void ShouldNotRethrowTheExceptionOnceHandledTest()
        {
            var target = new ErrorHandlingContext(this, ErrorSeverityLevel.NonFatal, new Exception());

            target.MarkErrorAsHandled();
            Assert.DoesNotThrow(() => target.RethrowErrorIfNotHandled());
        }
    }
}
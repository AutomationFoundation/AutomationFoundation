using System;
using NUnit.Framework;

namespace AutomationFoundation.Tests
{
    [TestFixture]
    public class ProcessingContextTests
    {
        [Test]
        public void ReturnsTheIdCorrectly()
        {
            Guid expected = Guid.NewGuid();
            var target = new ProcessingContext(expected);

            Assert.AreEqual(expected, target.Id);
        }

        [Test]
        public void SetsTheCurrentContext()
        {
            var expected = new ProcessingContext(Guid.NewGuid());
            ProcessingContext.SetCurrent(expected);

            var result = ProcessingContext.Current;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ProcessingContext.SetCurrent(null));
        }

        [Test]
        public void ClearsTheCurrentContext()
        {
            ProcessingContext.SetCurrent(new ProcessingContext(Guid.NewGuid()));
            Assert.IsNotNull(ProcessingContext.Current);

            ProcessingContext.Clear();

            Assert.IsNull(ProcessingContext.Current);
        }
    }
}

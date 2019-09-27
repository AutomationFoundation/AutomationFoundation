using System;
using AutomationFoundation.Runtime.Stubs;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Runtime
{
    [TestFixture]
    public class ProcessingContextTests
    {
        private Guid id;
        private Mock<IServiceScope> scope;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            scope = new Mock<IServiceScope>();
        }

        [Test]
        public void ReturnsTheIdCorrectly()
        {
            using (var target = new StubProcessingContext(id, scope.Object))
            {
                Assert.AreEqual(id, target.Id);
            }
        }

        [Test]
        public void SetsTheCurrentContext()
        {
            using (var expected = new StubProcessingContext(id, scope.Object))
            {
                ProcessingContext.SetCurrent(expected);

                var result = ProcessingContext.Current;

                Assert.AreEqual(expected, result);
            }
        }

        [Test]
        public void ThrowsAnExceptionWhenTheContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ProcessingContext.SetCurrent(null));
        }

        [Test]
        public void ClearsTheCurrentContext()
        {
            using (var target = new StubProcessingContext(id, scope.Object))
            {
                ProcessingContext.SetCurrent(target);
                Assert.IsNotNull(ProcessingContext.Current);

                ProcessingContext.Clear();

                Assert.IsNull(ProcessingContext.Current);
            }
        }

        [Test]
        public void DisposesTheScopeWhenTheContextIsDisposed()
        {
            using (var _ = new StubProcessingContext(id, scope.Object))
            {
                // This block intentionally left blank.
            }

            scope.Verify(o => o.Dispose(), Times.Once);
        }
    }
}

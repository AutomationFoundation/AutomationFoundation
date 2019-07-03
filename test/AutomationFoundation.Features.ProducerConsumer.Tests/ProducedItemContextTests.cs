using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests
{
    [TestFixture]
    public class ProducedItemContextTests
    {
        [Test]
        public void ThrowsAnExceptionIfTheItemIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ProducedItemContext(Guid.NewGuid(), null, new Mock<IServiceScope>().Object));
        }

        [Test]
        public void ThrowsAnExceptionIfTheScopeIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ProducedItemContext(Guid.NewGuid(), new Mock<IProducedItem>().Object, null));
        }

        [Test]
        public void ReturnsTheItemCorrectly()
        {
            var item = new Mock<IProducedItem>();
            var scope = new Mock<IServiceScope>();

            var target = new ProducedItemContext(Guid.NewGuid(), item.Object, scope.Object);

            Assert.AreEqual(item.Object, target.Item);
        }

        [Test]
        public void ReturnsTheScopeCorrectly()
        {
            var item = new Mock<IProducedItem>();
            var scope = new Mock<IServiceScope>();

            var target = new ProducedItemContext(Guid.NewGuid(), item.Object, scope.Object);

            Assert.AreEqual(scope.Object, target.Scope);
        }

        [Test]
        public void ReturnsTheIdCorrectly()
        {
            var id = Guid.NewGuid();
            var item = new Mock<IProducedItem>();
            var scope = new Mock<IServiceScope>();

            var target = new ProducedItemContext(id, item.Object, scope.Object);

            Assert.AreEqual(id, target.Id);
        }
    }
}

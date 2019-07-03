using System;
using System.Threading;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests
{
    [TestFixture]
    public class ConsumerRunnerTests
    {
        [Test]
        public void ThrowsAnExceptionWhenTheConsumerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ConsumerRunner<object>(null));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheItemIsNull()
        {
            var consumer = new Mock<IConsumer<object>>();

            var target = new ConsumerRunner<object>(consumer.Object);
            Assert.Throws<ArgumentNullException>(() => target.Run(null, CancellationToken.None));
        }

        [Test]
        public void RunsTheConsumerCorrectly()
        {
            var consumer = new Mock<IConsumer<object>>();
            var item = new Mock<IProducedItem>();
            item.Setup(o => o.GetItem<object>()).Returns((object)null);

            var target = new ConsumerRunner<object>(consumer.Object);
            target.Run(item.Object, CancellationToken.None);

            consumer.Verify(o => o.Consume(null, CancellationToken.None), Times.Once);
        }
    }
}

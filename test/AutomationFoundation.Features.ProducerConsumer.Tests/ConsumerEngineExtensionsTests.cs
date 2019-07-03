using System;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime.Abstractions;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests
{
    [TestFixture]
    public class ConsumerEngineExtensionsTests
    {
        [Test]
        public void ShouldNotGetTaskFromEngineIfStartSynchronous()
        {
            var engine = new Mock<IConsumerEngine>();

            var result = engine.Object.StartIfAsynchronous();

            Assert.AreEqual(Task.CompletedTask, result);
        }

        [Test]
        public void ShouldGetTaskFromEngineIfStartAsynchronous()
        {
            var expected = Task.CompletedTask;

            var engine = new Mock<IConsumerEngine>();
            var startable = engine.As<IStartable>();

            startable.Setup(o => o.StartAsync()).Returns(expected);

            var result = engine.Object.StartIfAsynchronous();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ThrowsAnExceptionWhenStartIfTheEngineIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ConsumerEngineExtensions.StartIfAsynchronous(null));
        }

        [Test]
        public void ShouldNotGetTaskFromEngineIfStopSynchronous()
        {
            var engine = new Mock<IConsumerEngine>();

            var result = engine.Object.StopIfAsynchronous();

            Assert.AreEqual(Task.CompletedTask, result);
        }

        [Test]
        public void ShouldGetTaskFromEngineIfStopAsynchronous()
        {
            var expected = Task.CompletedTask;

            var engine = new Mock<IConsumerEngine>();
            var stoppable = engine.As<IStoppable>();

            stoppable.Setup(o => o.StopAsync()).Returns(expected);

            var result = engine.Object.StopIfAsynchronous();

            Assert.AreEqual(expected, result);            
        }

        [Test]
        public void ThrowsAnExceptionWhenStopIfTheEngineIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ConsumerEngineExtensions.StopIfAsynchronous(null));
        }
    }
}
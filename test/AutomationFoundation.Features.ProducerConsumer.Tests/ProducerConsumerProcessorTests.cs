using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests
{
    [TestFixture]
    public class ProducerConsumerProcessorTests
    {
        [Test]
        public void MustStartTheProducerAndConsumerEnginesOnStart()
        {
            var producerEngine = new Mock<IProducerEngine>();
            var consumerEngine = new Mock<IAsynchronousConsumerEngine>();

            var target = new ProducerConsumerProcessor("Test", producerEngine.Object, consumerEngine.Object);
            target.Start();

            Assert.AreEqual(ProcessorState.Started, target.State);

            producerEngine.Verify(o => o.StartAsync(It.IsAny<ProducerEngineContext>()), Times.Once);
            consumerEngine.Verify(o => o.StartAsync(), Times.Once);
        }

        [Test]
        public void MustStopTheProducerAndConsumerEnginesOnStop()
        {
            var producerEngine = new Mock<IProducerEngine>();
            var consumerEngine = new Mock<IAsynchronousConsumerEngine>();

            var target = new ProducerConsumerProcessor("Test", producerEngine.Object, consumerEngine.Object);
            target.Start();
            
            Assert.AreEqual(ProcessorState.Started, target.State);

            target.Stop();

            Assert.AreEqual(ProcessorState.Stopped, target.State);

            producerEngine.Verify(o => o.StopAsync(), Times.Once);
            consumerEngine.Verify(o => o.StopAsync(), Times.Once);
        }
    }
}
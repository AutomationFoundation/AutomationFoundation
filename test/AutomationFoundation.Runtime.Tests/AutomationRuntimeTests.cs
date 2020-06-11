using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Runtime
{
    [TestFixture]
    public class AutomationRuntimeTests
    {
        private Mock<IProcessor> processor;

        [SetUp]
        public void Setup()
        {
            processor = new Mock<IProcessor>();
        }

        [Test]
        public void ReturnsTheProcessorsThatHaveBeenAdded()
        {
            using var target = new AutomationRuntime();
            target.Add(processor.Object);

            Assert.True(target.Processors.Contains(processor.Object));
        }

        [Test]
        public void IdentifiesTrueIfTheProcessorIsStarted()
        {
            processor.Setup(o => o.State).Returns(ProcessorState.Started);

            using var target = new AutomationRuntime();
            target.Add(processor.Object);

            Assert.True(target.IsRunning);
        }

        [Test]
        public void IdentifiesTrueIfTheProcessorIsBusy()
        {
            processor.Setup(o => o.State).Returns(ProcessorState.Busy);

            using var target = new AutomationRuntime();
            target.Add(processor.Object);

            Assert.True(target.IsRunning);
        }

        [Test]
        public void ReturnsTrueWhenTheProcessorHasBeenAdded()
        {
            using var target = new AutomationRuntime();
            var result = target.Add(processor.Object);

            Assert.True(result);
        }

        [Test]
        public void ReturnsFalseWhenTheProcessorHasAlreadyBeenAdded()
        {
            using var target = new AutomationRuntime();
            target.Add(processor.Object);

            var result = target.Add(processor.Object);
            Assert.False(result);
        }

        [Test]
        public void ReturnsTrueWhenTheProcessorHasBeenRemoved()
        {
            using var target = new AutomationRuntime();
            target.Add(processor.Object);

            var result = target.Remove(processor.Object);
            Assert.True(result);
        }

        [Test]
        public void ReturnsFalseWhenTheProcessorHasNotBeenAdded()
        {
            using var target = new AutomationRuntime();
            var result = target.Remove(processor.Object);

            Assert.False(result);
        }

        [Test]
        public void ThrowsAnExceptionIfTheProcessorIsNullWhenAdded()
        {
            using var target = new AutomationRuntime();
            Assert.Throws<ArgumentNullException>(() => target.Add(null));
        }

        [Test]
        public void ThrowsAnExceptionIfTheProcessorIsNullWhenRemoved()
        {
            using var target = new AutomationRuntime();
            Assert.Throws<ArgumentNullException>(() => target.Remove(null));
        }

        [Test]
        public async Task StartsTheProcessor()
        {
            using var target = new AutomationRuntime();
            target.Add(processor.Object);

            await target.StartAsync();

            processor.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task StopsTheProcessor()
        {
            using var target = new AutomationRuntime();
            target.Add(processor.Object);

            await target.StopAsync();

            processor.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void ThrowAnExceptionWhenStartedAfterDisposed()
        {
            var target = new AutomationRuntime();
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.StartAsync());
        }

        [Test]
        public void ThrowAnExceptionWhenStoppedAfterDisposed()
        {
            var target = new AutomationRuntime();
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.StopAsync());
        }
    }
}
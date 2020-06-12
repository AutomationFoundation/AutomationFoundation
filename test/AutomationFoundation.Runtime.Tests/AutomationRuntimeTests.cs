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
        private AutomationRuntime target;
        private CancellationTokenSource cancellationSource;
        private Mock<IProcessor> processor1;
        private Mock<IProcessor> processor2;

        [SetUp]
        public void Setup()
        {
            target = new AutomationRuntime();
            
            processor1 = new Mock<IProcessor>();
            processor2 = new Mock<IProcessor>();
            cancellationSource = new CancellationTokenSource();
        }

        [TearDown]
        public void Cleanup()
        {
            target?.Dispose();
            cancellationSource?.Dispose();
        }

        [Test]
        public void ReturnsTheProcessorsThatHaveBeenAdded()
        {
            target.Add(processor1.Object);

            Assert.True(target.Processors.Contains(processor1.Object));
        }

        [Test]
        public void IdentifiesTrueIfTheProcessorIsStarted()
        {
            processor1.Setup(o => o.State).Returns(ProcessorState.Started);

            target.Add(processor1.Object);

            Assert.True(target.IsRunning);
        }

        [Test]
        public void IdentifiesTrueIfTheProcessorIsBusy()
        {
            processor1.Setup(o => o.State).Returns(ProcessorState.Busy);

            target.Add(processor1.Object);

            Assert.True(target.IsRunning);
        }

        [Test]
        public void ReturnsTrueWhenTheProcessorHasBeenAdded()
        {
            var result = target.Add(processor1.Object);

            Assert.True(result);
        }

        [Test]
        public void ReturnsFalseWhenTheProcessorHasAlreadyBeenAdded()
        {
            target.Add(processor1.Object);

            var result = target.Add(processor1.Object);
            Assert.False(result);
        }

        [Test]
        public void ReturnsTrueWhenTheProcessorHasBeenRemoved()
        {
            target.Add(processor1.Object);

            var result = target.Remove(processor1.Object);
            Assert.True(result);
        }

        [Test]
        public void ReturnsFalseWhenTheProcessorHasNotBeenAdded()
        {
            var result = target.Remove(processor1.Object);

            Assert.False(result);
        }

        [Test]
        public void ThrowsAnExceptionIfTheProcessorIsNullWhenAdded()
        {
            Assert.Throws<ArgumentNullException>(() => target.Add(null));
        }

        [Test]
        public void ThrowsAnExceptionIfTheProcessorIsNullWhenRemoved()
        {
            Assert.Throws<ArgumentNullException>(() => target.Remove(null));
        }

        /// <summary>
        /// When cancellation occurs while starting the runtime, 
        /// </summary>
        [Test]
        public void ThrowsAnExceptionWhenCanceledWhileStartingTheProcessors()
        {
            cancellationSource.Cancel(); // Need to preemptively cancel as the 

            target.Add(processor1.Object);
            target.Add(processor2.Object);

            Assert.ThrowsAsync<OperationCanceledException>(async () => await target.StartAsync(cancellationSource.Token));
        }

        [Test]
        public async Task StartsTheProcessors()
        {
            target.Add(processor1.Object);
            target.Add(processor2.Object);

            await target.StartAsync();

            processor1.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()));
            processor2.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task StopsTheProcessors()
        {
            target.Add(processor1.Object);
            target.Add(processor2.Object);

            await target.StopAsync();

            processor1.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()));
            processor2.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()));
        }

        [Test]
        public void ThrowAnExceptionWhenStartedAfterDisposed()
        {
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.StartAsync());
        }

        [Test]
        public void ThrowAnExceptionWhenStoppedAfterDisposed()
        {
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.StopAsync());
        }
    }
}
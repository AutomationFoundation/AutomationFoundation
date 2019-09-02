using System;
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
        public void IdentifiesTrueIfTheProcessorIsStarted()
        {
            processor.Setup(o => o.State).Returns(ProcessorState.Started);

            using (var target = new AutomationRuntime())
            {
                target.Processors.Add(processor.Object);

                Assert.True(target.IsActive);
            }
        }

        [Test]
        public void IdentifiesTrueIfTheProcessorIsBusy()
        {
            processor.Setup(o => o.State).Returns(ProcessorState.Busy);

            using (var target = new AutomationRuntime())
            {
                target.Processors.Add(processor.Object);

                Assert.True(target.IsActive);
            }
        }

        [Test]
        public void StartsTheProcessor()
        {
            using (var target = new AutomationRuntime())
            {
                target.Processors.Add(processor.Object);
                target.Start();

                processor.Verify(o => o.Start(), Times.Once);
            }
        }

        [Test]
        public void StopsTheProcessor()
        {
            using (var target = new AutomationRuntime())
            {
                target.Processors.Add(processor.Object);
                target.Stop();

                processor.Verify(o => o.Stop(), Times.Once);
            }
        }

        [Test]
        public void ThrowAnExceptionWhenStartedAfterDisposed()
        {
            var target = new AutomationRuntime();
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Start());
        }

        [Test]
        public void ThrowAnExceptionWhenStoppedAfterDisposed()
        {
            var target = new AutomationRuntime();
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Stop());
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.NETCore.App
{
    [TestFixture]
    public class SigTermRuntimeHostRunAsyncStrategyTests
    {
        private TestableSigTermRuntimeHostRunAsyncStrategy target;

        private Mock<ILogger<SigTermRuntimeHostRunAsyncStrategy>> logger;
        private Mock<IOptions<SigTermRuntimeHostRunAsyncOptions>> optionsWrapper;
        private SigTermRuntimeHostRunAsyncOptions options;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<SigTermRuntimeHostRunAsyncStrategy>>();

            options = new SigTermRuntimeHostRunAsyncOptions();
            
            optionsWrapper = new Mock<IOptions<SigTermRuntimeHostRunAsyncOptions>>();
            optionsWrapper.Setup(o => o.Value).Returns(options);

            target = new TestableSigTermRuntimeHostRunAsyncStrategy(logger.Object, optionsWrapper.Object);
        }

        [TearDown]
        public void Finish()
        {
            target?.Dispose();
        }

        [Test]
        public void ThrowsAnExceptionWhenLoggerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new SigTermRuntimeHostRunAsyncStrategy(null, optionsWrapper.Object));
        }

        [Test]
        public async Task FlagForCancellationWhenKeyPressed()
        {
            await target.SimulateProcessExitedAsync();

            Assert.True(target.FlaggedForCancellation);
        }

        [Test]
        public async Task AttachesToTheCtrlCKeyPressEventWhenRan()
        {
            await target.SimulateBeingRanAsync();

            Assert.True(target.AttachedToProcessExitEvent);
        }

        [Test]
        public async Task StopsAsExpected()
        {
            await target.SimulateBeingStoppingAsync();

            Assert.True(target.Stopped);
        }
    }
}
using System;
using System.Threading.Tasks;
using AutomationFoundation.TestObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation
{
    [TestFixture]
    public class CtrlCRuntimeHostRunAsyncStrategyTests
    {
        private TestableCtrlCRuntimeHostRunAsyncStrategy target;

        private Mock<ILogger<CtrlCRuntimeHostRunAsyncStrategy>> logger;
        private Mock<IOptions<CtrlCRuntimeHostRunAsyncOptions>> optionsWrapper;
        private CtrlCRuntimeHostRunAsyncOptions options;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<CtrlCRuntimeHostRunAsyncStrategy>>();

            options = new CtrlCRuntimeHostRunAsyncOptions();
            
            optionsWrapper = new Mock<IOptions<CtrlCRuntimeHostRunAsyncOptions>>();
            optionsWrapper.Setup(o => o.Value).Returns(options);

            target = new TestableCtrlCRuntimeHostRunAsyncStrategy(logger.Object, optionsWrapper.Object);
        }

        [TearDown]
        public void Finish()
        {
            target?.Dispose();
        }

        [Test]
        public void ThrowsAnExceptionWhenOptionsWrapperIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new CtrlCRuntimeHostRunAsyncStrategy(logger.Object, (IOptions<CtrlCRuntimeHostRunAsyncOptions>)null));
        }

        [Test]
        public void ThrowsAnExceptionWhenOptionsWrapperValueIsNull()
        {
            optionsWrapper.Setup(o => o.Value).Returns((CtrlCRuntimeHostRunAsyncOptions) null);

            Assert.Throws<ArgumentNullException>(() => _ = new CtrlCRuntimeHostRunAsyncStrategy(logger.Object, (IOptions<CtrlCRuntimeHostRunAsyncOptions>)null));
        }

        [Test]
        public void ThrowsAnExceptionWhenLoggerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new CtrlCRuntimeHostRunAsyncStrategy(null, optionsWrapper.Object));
        }

        [Test]
        public async Task FlagForCancellationWhenKeyPressed()
        {
            var cancel = await target.SimulateCtrlCKeyPressedAsync();

            Assert.True(cancel);
            Assert.True(target.FlaggedForCancellation);
        }

        [Test]
        public async Task AttachesToTheCtrlCKeyPressEventWhenRan()
        {
            await target.SimulateBeingRanAsync();

            Assert.True(target.AttachedToCtrlCKeyPressEvent);
        }

        [Test]
        public async Task StopsAsExpected()
        {
            await target.SimulateBeingStoppingAsync();

            Assert.True(target.Stopped);
        }
    }
}
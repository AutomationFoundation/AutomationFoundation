using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Hosting;
using AutomationFoundation.TestObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation
{
    [TestFixture]
    public class RuntimeHostRunAsyncStrategyTests
    {
        private Mock<IRuntimeHost> host;
        private TestableRuntimeHostRunAsyncStrategy target;
        private RuntimeHostRunAsyncOptions options;

        [SetUp]
        public void Setup()
        {
            options = new RuntimeHostRunAsyncOptions();
            
            host = new Mock<IRuntimeHost>();
            target = new TestableRuntimeHostRunAsyncStrategy(new OptionsWrapper<RuntimeHostRunAsyncOptions>(options));
        }

        [TearDown]
        public void Finish()
        {
            target.Dispose();
        }

        [Test]
        public void ThrowsAnExceptionWhenOptionsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new TestableRuntimeHostRunAsyncStrategy(null));
        }

        [Test]
        public void ThrowsAnExceptionWhenOptionsWrapperValueIsNull()
        {
            var optionsWrapper = new Mock<IOptions<RuntimeHostRunAsyncOptions>>();
            optionsWrapper.Setup(o => o.Value).Returns((RuntimeHostRunAsyncOptions) null);

            Assert.Throws<ArgumentNullException>(() => _ = new TestableRuntimeHostRunAsyncStrategy(optionsWrapper.Object));
        }

        [Test]
        public async Task SwallowTheCancellationExceptionWhileStarting()
        {
            target.Cancel();
            
            await target.RunAsync(host.Object);

            Assert.True(target.CanceledWhileRunning);
        }

        [Test]
        public async Task SwallowTheCancellationExceptionWhenTimedOutWhileStarting()
        {
            host.Setup(o => o.StartAsync(It.IsAny<CancellationToken>())).Callback<CancellationToken>(
                async (cancelToken) =>
                {
                    await Task.Delay(Timeout.InfiniteTimeSpan, cancelToken);
                }).Throws<OperationCanceledException>();

            await target.RunAsync(host.Object);

            host.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()));
            host.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()));

            Assert.True(target.CanceledWhileRunning);
        }

        [Test]
        public async Task SwallowTheCancellationExceptionWhenTimedOutWhileStopping()
        {
            host.Setup(o => o.StopAsync(It.IsAny<CancellationToken>())).Callback<CancellationToken>(
                async (cancelToken) =>
                {
                    await Task.Delay(Timeout.InfiniteTimeSpan, cancelToken);
                }).Throws<OperationCanceledException>();

            target.CancelAfter(TimeSpan.FromSeconds(1));

            await target.RunAsync(host.Object);

            host.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()));
            host.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()));

            Assert.True(target.CanceledWhileStopping);
        }
    }
}
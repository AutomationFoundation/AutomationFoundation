using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Hosting.Abstractions;
using AutomationFoundation.Hosting.TestObjects;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class RuntimeHostRunAsyncStrategyTests
    {
        private Mock<IRuntimeHost> host;
        private TestableRuntimeHostRunAsyncStrategy target;

        [SetUp]
        public void Setup()
        {
            host = new Mock<IRuntimeHost>();
            target = new TestableRuntimeHostRunAsyncStrategy();
        }

        [TearDown]
        public void Finish()
        {
            target.Dispose();
        }

        [Test]
        public async Task SwallowTheCancellationExceptionWhileStarting()
        {
            target.Cancel();
            
            await target.RunAsync(host.Object, 1000, 1000);

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

            await target.RunAsync(host.Object, 100, 100);

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

            await target.RunAsync(host.Object, Timeout.Infinite, 100);

            host.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()));
            host.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()));

            Assert.True(target.CanceledWhileStopping);
        }
    }
}
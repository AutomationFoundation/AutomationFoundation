using System.Threading.Tasks;
using AutomationFoundation.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class RuntimeHostExtensionsTests
    {
        private Mock<IRuntimeHost> host;
        private Mock<IRuntimeHostRunAsyncStrategy> runStrategy;

        [SetUp]
        public void Setup()
        {
            runStrategy = new Mock<IRuntimeHostRunAsyncStrategy>();

            var services = new ServiceCollection();
            services.AddSingleton(sp => runStrategy.Object);

            host = new Mock<IRuntimeHost>();
            host.Setup(o => o.ApplicationServices).Returns(services.BuildServiceProvider());
        }

        [Test]
        public async Task ResolveAndForwardTheRequest()
        {
            var target = host.Object;

            await target.RunAsync();

            runStrategy.Verify(o => o.RunAsync(target, It.IsAny<int>(), It.IsAny<int>()));
        }

        //[Test]
        //public async Task SwallowTheCancellationExceptionWhileStarting()
        //{
        //    var target = host.Object;

        //    await target.RunAsync();

        //    host.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()));
        //    host.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()));
        //}

        //[Test]
        //public async Task SwallowTheCancellationExceptionWhenTimedOutWhileStarting()
        //{
        //    var canceled = false;

        //    host.Setup(o => o.StartAsync(It.IsAny<CancellationToken>())).Callback<CancellationToken>(async (cancelToken) =>
        //    {
        //        try
        //        {
        //            await Task.Delay(Timeout.InfiniteTimeSpan, cancelToken);
        //        }
        //        catch (OperationCanceledException)
        //        {
        //            canceled = true;
        //        }
        //    }).Throws<OperationCanceledException>();

        //    var target = host.Object;

        //    await target.RunAsync(CancellationToken.None, 100);

        //    host.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()));
        //    host.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()));

        //    Assert.True(canceled);
        //}

        //[Test]
        //public async Task SwallowTheCancellationExceptionWhenTimedOutWhileStopping()
        //{
        //    var canceled = false;

        //    host.Setup(o => o.StopAsync(It.IsAny<CancellationToken>())).Callback<CancellationToken>(async (cancelToken) =>
        //    {
        //        try
        //        {
        //            await Task.Delay(Timeout.InfiniteTimeSpan, cancelToken);
        //        }
        //        catch (OperationCanceledException)
        //        {
        //            canceled = true;
        //        }
        //    }).Throws<OperationCanceledException>();

        //    var target = host.Object;

        //    await target.RunAsync(new CancellationToken(true), shutdownTimeoutMs: 100);

        //    host.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()));
        //    host.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()));

        //    Assert.True(canceled);
        //}
    }
}
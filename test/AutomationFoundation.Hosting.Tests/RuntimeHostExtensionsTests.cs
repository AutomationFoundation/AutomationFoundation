using System;
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
        private IServiceCollection services;
        private Mock<IRuntimeHost> host;
        private Mock<IRuntimeHostRunAsyncStrategy> runStrategy;

        [SetUp]
        public void Setup()
        {
            runStrategy = new Mock<IRuntimeHostRunAsyncStrategy>();

            services = new ServiceCollection();
            services.AddSingleton(sp => runStrategy.Object);

            host = new Mock<IRuntimeHost>();
        }

        [Test]
        public async Task ResolveAndForwardTheRequest()
        {
            host.Setup(o => o.ApplicationServices).Returns(services.BuildServiceProvider());

            var target = host.Object;

            await target.RunAsync();

            runStrategy.Verify(o => o.RunAsync(target, It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void ThrowsAnExceptionWhenRunStrategyHasNotBeenConfigured()
        {
            var sp = new Mock<IServiceProvider>();
            host.Setup(o => o.ApplicationServices).Returns(sp.Object);

            var target = host.Object;

            Assert.ThrowsAsync<HostingException>(async () => await target.RunAsync());
        }
    }
}
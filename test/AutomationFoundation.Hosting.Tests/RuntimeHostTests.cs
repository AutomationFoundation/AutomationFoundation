using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class RuntimeHostTests
    {
        private Mock<IRuntime> runtime;
        private Mock<IHostingEnvironment> environment;
        private Mock<IServiceProvider> services;

        private RuntimeHost target;

        [SetUp]
        public void Setup()
        {
            runtime = new Mock<IRuntime>();
            environment = new Mock<IHostingEnvironment>();
            services = new Mock<IServiceProvider>();

            target = new RuntimeHost(runtime.Object, environment.Object, services.Object);
        }

        [Test]
        public void ThrowsAnExceptionWhenRuntimeIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RuntimeHost(null, environment.Object, services.Object));
        }

        [Test]
        public void ThrowsAnExceptionWhenEnvironmentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RuntimeHost(runtime.Object, null, services.Object));
        }

        [Test]
        public void DoesNotThrowsAnExceptionWhenServicesAreNull()
        {
            Assert.DoesNotThrow(() => new RuntimeHost(runtime.Object, environment.Object, null));
        }

        [Test]
        public async Task StartsTheRuntime()
        {
            await target.StartAsync();

            runtime.Verify(o => o.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task StopsTheRuntime()
        {
            await target.StopAsync();

            runtime.Verify(o => o.StopAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void ReturnsTheServices()
        {
            Assert.AreSame(services.Object, target.ApplicationServices);
        }

        [Test]
        public void ReturnsTheEnvironment()
        {
            Assert.AreSame(environment.Object, target.Environment);
        }

        [Test]
        public void DisposesTheRuntime()
        {
            target.Dispose();

            runtime.Verify(o => o.Dispose());
        }
    }
}
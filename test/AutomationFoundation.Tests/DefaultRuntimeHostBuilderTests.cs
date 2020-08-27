using System;
using AutomationFoundation.Hosting.TestObjects;
using AutomationFoundation.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation
{
    [TestFixture]
    public class DefaultRuntimeHostBuilderTests
    {
        private TestableRuntimeHostBuilder target;

        [SetUp]
        public void Init()
        {
            target = new TestableRuntimeHostBuilder();;
        }

        [Test]
        public void ThrowsAnExceptionWhenTheCallbackIsNullWhileConfiguringThe0Environment()
        {
            Assert.Throws<ArgumentNullException>(() => target.ConfigureHostingEnvironment(null));
        }

        [Test]
        public void ThrowAnExceptionWhenStartupHasNotBeenConfigured()
        {
            var ex = Assert.Throws<BuildException>(() => target.Build());

            Assert.AreEqual("The startup must be configured.", ex.Message);
        }

        [Test]
        public void ThrowsAnExceptionWhenCallbackIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => target.ConfigureServices((Action<IServiceCollection>)null));
        }

        [Test]
        public void ThrowsAnExceptionWhenStartupInstanceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => target.UseStartup(null));
        }

        [Test]
        public void ThrowAnExceptionWhenStartupIsNotResolved()
        {
            var target = new TestableRuntimeHostBuilder(sp => null);
            target.UseStartup<StubStartup>();

            var ex = Assert.Throws<BuildException>(() => target.Build());
            Assert.AreEqual("The startup instance was not resolved.", ex.Message);
        }

        [Test]
        public void ThrowAnExceptionWhenApplicationServicesIsNotReturned()
        {
            var target = new TestableRuntimeHostBuilder(applicationServicesResolver: (sp, sc) => null);
            target.UseStartup<StubStartup>();

            var ex = Assert.Throws<BuildException>(() => target.Build());
            Assert.AreEqual("The services were not configured.", ex.Message);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheRuntimeIsNull()
        {
            var builder = new Mock<IRuntimeBuilder>();

            var target = new TestableRuntimeHostBuilder(runtimeBuilderResolver: _ => builder.Object);
            target.UseStartup<StubStartup>();

            var ex = Assert.Throws<BuildException>(() => target.Build());
            Assert.AreEqual("The runtime could not be built.", ex.Message);

            builder.Verify(o => o.Build(), Times.Once);
        }

        [Test]
        public void ShouldRunTheCallbackWhenBeingBuilt()
        {
            var called = false;

            target.ConfigureServices(services => { called = true; });
            target.UseStartup(new StubStartup());

            Assert.IsNotNull(target.Build());
            Assert.True(called);
        }

        [Test]
        public void MustConfigureProcessorsWhenBuilt()
        {
            var called = false;

            var target = new RuntimeHostBuilder();
            target.UseStartup(new StubStartup(_ => called = true));

            Assert.IsNotNull(target.Build());
            Assert.True(called);
        }
    }
}
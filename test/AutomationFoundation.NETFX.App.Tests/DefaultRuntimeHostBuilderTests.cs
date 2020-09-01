using System;
using AutomationFoundation.Runtime;
using AutomationFoundation.TestObjects;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AutomationFoundation
{
    [TestFixture]
    public class DefaultRuntimeHostBuilderTests
    {
        private DefaultRuntimeHostBuilder target;

        [SetUp]
        public void Init()
        {
            target = new DefaultRuntimeHostBuilder();
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

            target.UseStartup(new StubStartup(_ => called = true));

            Assert.IsNotNull(target.Build());
            Assert.True(called);
        }
    }
}
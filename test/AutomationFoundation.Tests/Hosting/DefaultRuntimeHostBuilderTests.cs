using AutomationFoundation.Hosting;
using AutomationFoundation.Runtime.Builder;
using AutomationFoundation.Tests.Stubs;
using NUnit.Framework;

namespace AutomationFoundation.Tests.Hosting
{
    [TestFixture]
    public class DefaultRuntimeHostBuilderTests
    {
        [Test]
        public void ThrowAnExceptionWhenStartupHasNotBeenConfigured()
        {
            var target = new DefaultRuntimeHostBuilder();
            Assert.Throws<BuildException>(() => target.Build(), "The startup must be configured.");
        }
        
        [Test]
        public void ShouldRunTheCallbackWhenBeingBuilt()
        {
            var called = false;

            var target = new DefaultRuntimeHostBuilder();
            target.ConfigureServices(services => { called = true; });
            target.UseStartup(new StubStartup());

            Assert.IsNotNull(target.Build());
            Assert.True(called);
        }

        [Test]
        public void MustConfigureProcessorsWhenBuilt()
        {
            var called = false;

            var target = new DefaultRuntimeHostBuilder();
            target.UseStartup(new StubStartup(_ => called = true));

            Assert.IsNotNull(target.Build());
            Assert.True(called);
        }
    }
}
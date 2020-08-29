using AutomationFoundation.Hosting.TestObjects;
using AutomationFoundation.Runtime;
using AutomationFoundation.TestObjects;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation
{
    [TestFixture]
    public class RuntimeHostBuilderTests
    {
        private TestableRuntimeHostBuilder target;

        [SetUp]
        public void Init()
        {
            target = new TestableRuntimeHostBuilder();
        }

        [Test]
        public void ThrowAnExceptionWhenStartupIsNotResolved()
        {
            target.StartupResolver = sp => null;
            target.UseStartup<StubStartup>();

            var ex = Assert.Throws<BuildException>(() => target.Build());
            Assert.AreEqual("The startup instance was not resolved.", ex.Message);
        }

        [Test]
        public void ThrowAnExceptionWhenApplicationServicesIsNotReturned()
        {
            target.ApplicationServicesResolver = (sp, sc) => null;
            target.UseStartup<StubStartup>();

            var ex = Assert.Throws<BuildException>(() => target.Build());
            Assert.AreEqual("The services were not configured.", ex.Message);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheRuntimeIsNull()
        {
            var runtimeBuilder = new Mock<IRuntimeBuilder>();
            runtimeBuilder.Setup(o => o.Build()).Returns((IRuntime) null);

            target.RuntimeBuilderResolver = sp => runtimeBuilder.Object;
            target.UseStartup<StubStartup>();

            var ex = Assert.Throws<BuildException>(() => target.Build());
            Assert.AreEqual("The runtime could not be built.", ex.Message);
        }
    }
}
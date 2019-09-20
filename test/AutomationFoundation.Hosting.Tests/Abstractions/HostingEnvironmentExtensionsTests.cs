using AutomationFoundation.Hosting.Stubs;
using NUnit.Framework;

namespace AutomationFoundation.Hosting.Abstractions
{
    [TestFixture]
    public class HostingEnvironmentExtensionsTests
    {
        [Test]
        public void ReturnTrueForProduction()
        {
            var target = new StubHostingEnvironment("Production");
            var result = target.IsProduction();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForStaging()
        {
            var target = new StubHostingEnvironment("Staging");
            var result = target.IsStaging();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForStaging2()
        {
            var target = new StubHostingEnvironment("Staging_2");
            var result = target.IsStaging();

            Assert.True(result);
        }

        [Test]
        public void ReturnTrueForDevelopment()
        {
            var target = new StubHostingEnvironment("Development");
            var result = target.IsDevelopment();

            Assert.True(result);
        }
    }
}
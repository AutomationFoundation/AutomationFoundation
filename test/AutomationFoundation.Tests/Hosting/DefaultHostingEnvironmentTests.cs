using AutomationFoundation.Hosting.Stubs;
using NUnit.Framework;

namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class DefaultHostingEnvironmentTests
    {
        [Test]
        public void ReturnsTheEnvironmentVariableValueAsExpected()
        {
            var expected = "Environment";

            var target = new TestableDefaultHostingEnvironment(expected);
            var actual = target.EnvironmentName;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DefaultsTheEnvironmentToDevelopmentWhenNotSpecified()
        {
            var target = new TestableDefaultHostingEnvironment(null);
            var actual = target.EnvironmentName;

            Assert.AreEqual("Development", actual);
        }

        [Test]
        public void ReturnsTheEnvironmentVariableAsExpected()
        {
            var target = new DefaultHostingEnvironment();
            var environmentName = target.EnvironmentName;

            Assert.NotNull(environmentName);
        }
    }
}
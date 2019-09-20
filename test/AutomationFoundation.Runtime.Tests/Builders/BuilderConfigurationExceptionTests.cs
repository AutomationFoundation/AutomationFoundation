using NUnit.Framework;

namespace AutomationFoundation.Runtime.Builders
{
    [TestFixture]
    public class BuilderConfigurationExceptionTests
    {
        [Test]
        public void ReturnsTheMessage()
        {
            var expected = "An exception occurred.";

            var target = new BuilderConfigurationException(expected);
            Assert.AreEqual(expected, target.Message);
        }

        [Test]
        public void ReturnsTheException()
        {
            var expected = new BuilderConfigurationException();

            var target = new BuilderConfigurationException("An exception occurred.", expected);
            Assert.AreEqual(expected, target.InnerException);
        }
    }
}
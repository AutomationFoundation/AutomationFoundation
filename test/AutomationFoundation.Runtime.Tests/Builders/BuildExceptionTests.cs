using NUnit.Framework;

namespace AutomationFoundation.Runtime.Builders
{
    [TestFixture]
    public class BuildExceptionTests
    {
        [Test]
        public void ReturnsTheMessage()
        {
            var expected = "An exception occurred.";

            var target = new BuildException(expected);
            Assert.AreEqual(expected, target.Message);
        }

        [Test]
        public void ReturnsTheException()
        {
            var expected = new BuildException();

            var target = new BuildException("An exception occurred.", expected);
            Assert.AreEqual(expected, target.InnerException);
        }
    }
}
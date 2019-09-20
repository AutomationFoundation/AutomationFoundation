using NUnit.Framework;

namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class DefaultRuntimeHostTests
    {
        [Test]
        public void ReturnsADefaultHostBuilder()
        {
            var result = DefaultRuntimeHost.CreateBuilder();
            Assert.IsInstanceOf<DefaultRuntimeHostBuilder>(result);
        }
    }
}
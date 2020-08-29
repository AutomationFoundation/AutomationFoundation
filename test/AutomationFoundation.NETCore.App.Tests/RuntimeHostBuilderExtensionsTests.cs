using System;
using NUnit.Framework;

namespace AutomationFoundation
{
    [TestFixture]
    public class RuntimeHostBuilderExtensionsTests
    {
        [Test]
        public void ThrowAnExceptionWhenTheBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => RuntimeHostBuilderExtensions
                .ConfigureAppConfiguration(null, b => { }));
        }
    }
}
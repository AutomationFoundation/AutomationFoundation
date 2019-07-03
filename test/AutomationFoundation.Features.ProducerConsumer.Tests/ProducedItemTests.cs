using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests
{
    [TestFixture]
    public class ProducedItemTests
    {
        [Test]
        public void DoesNotThrowAnExceptionIfItemIsNull()
        {
            Assert.DoesNotThrow(() => new ProducedItem(null));
        }

        [Test]
        public void ReturnsTheItemCorrectly()
        {
            var expected = new object();

            var target = new ProducedItem(expected);
            var result = target.GetItem<object>();

            Assert.AreEqual(expected, result);
        }
    }
}

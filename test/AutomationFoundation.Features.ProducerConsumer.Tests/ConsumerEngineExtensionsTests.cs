using System;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.TestObjects;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer
{
    [TestFixture]
    public class ConsumerEngineExtensionsTests
    {
        [Test]
        public async Task StopTheEngineWhenStoppable()
        {
            var target = new Mock<TestableConsumerEngine>();
            await target.Object.StopIfAsynchronous();

            target.Verify(o => o.StopAsync());
        }

        [Test]
        public async Task StartTheEngineWhenStartable()
        {
            var target = new Mock<TestableConsumerEngine>();
            await target.Object.StartIfAsynchronous();

            target.Verify(o => o.StartAsync());
        }

        [Test]
        public void ThrowsAnExceptionWhenEngineIsNullWhileStart()
        {
            IConsumerEngine<int> target = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                // ReSharper disable once ExpressionIsAlwaysNull
                await target.StartIfAsynchronous());
        }

        [Test]
        public void ThrowsAnExceptionWhenEngineIsNullWhileStop()
        {
            IConsumerEngine<int> target = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                // ReSharper disable once ExpressionIsAlwaysNull
                await target.StopIfAsynchronous());
        }
    }
}
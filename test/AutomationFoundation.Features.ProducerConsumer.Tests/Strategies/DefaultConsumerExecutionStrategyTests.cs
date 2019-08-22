using System;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Strategies;
using AutomationFoundation.Features.ProducerConsumer.Tests.Strategies.Stubs;
using AutomationFoundation.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests.Strategies
{
    [TestFixture]
    public class DefaultConsumerExecutionStrategyTests
    {
        private Mock<IServiceScope> lifetimeScope;
        private ProducerConsumerContext<object> context;
        private Mock<IConsumer<object>> consumer;
        private Mock<IConsumerFactory<object>> consumerFactory;

        [SetUp]
        public void Setup()
        {
            lifetimeScope = new Mock<IServiceScope>();
            context = new ProducerConsumerContext<object>(Guid.NewGuid(), lifetimeScope.Object);

            consumer = new Mock<IConsumer<object>>();
            consumerFactory = new Mock<IConsumerFactory<object>>();
            consumerFactory.Setup(o => o.Create(lifetimeScope.Object)).Returns(consumer.Object);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheConsumerFactoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultConsumerExecutionStrategy<object>(null));
        }

        [Test]
        public void ThrowsExceptionWhenContextIsNull()
        {
            var target = new StubDefaultConsumerExecutionStrategy<object>(consumerFactory.Object);
            
            Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(null));
        }

        [Test]
        public void ThrowsExceptionWhenContextIsNull1()
        {
            var target = new StubDefaultConsumerExecutionStrategy<object>(consumerFactory.Object);
            target.SetOverrideContext(null);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(context));
        }

        [Test]
        public void ThrowsExceptionWhenContextIsNull2()
        {
            var target = new StubDefaultConsumerExecutionStrategy<object>(consumerFactory.Object, (strategy, context) =>
            {
                strategy.SetOverrideContext(null);
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(context));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheConsumerIsNull()
        {
            consumerFactory.Setup(o => o.Create(lifetimeScope.Object)).Returns((IConsumer<object>)null);

            var target = new StubDefaultConsumerExecutionStrategy<object>(consumerFactory.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(context));
        }

        [Test]
        public void ReleasesTheProcessingContextWhenFailsToCreateConsumer()
        {
            consumerFactory.Setup(o => o.Create(lifetimeScope.Object)).Throws<InvalidOperationException>();

            var target = new StubDefaultConsumerExecutionStrategy<object>(consumerFactory.Object, onExitCallback: (strategy, context) => {
                Assert.IsNull(ProcessingContext.Current);
            });

            Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(context));
        }

        [Test]
        public async Task ExecutesAllExtensionMethodsInOrderAsExpected()
        {
            bool called1 = false, called2 = false, called3 = false, called4 = false;

            var target = new StubDefaultConsumerExecutionStrategy<object>(consumerFactory.Object,
                (strategy, context) =>
                {
                    called1 = true;

                    Assert.IsFalse(called2);
                    Assert.IsFalse(called3);
                    Assert.IsFalse(called4);
                },
                (strategy, context) =>
                {
                    called2 = true;

                    Assert.IsTrue(called1);
                    Assert.IsFalse(called3);
                    Assert.IsFalse(called4);
                },
                (strategy, context) =>
                {
                    called3 = true; 

                    Assert.IsTrue(called1);
                    Assert.IsTrue(called2);
                    Assert.IsFalse(called4);
                },
                (strategy, context) =>
                {
                    called4 = true;

                    Assert.IsTrue(called1);
                    Assert.IsTrue(called2);
                    Assert.IsTrue(called3);
                });

            await target.ExecuteAsync(context);

            Assert.True(called1);
            Assert.True(called2);
            Assert.True(called3);
            Assert.True(called4);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Strategies;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests.Strategies
{
    [TestFixture]
    public class DefaultProducerExecutionStrategyTests
    {
        private Mock<IServiceScopeFactory> scopeFactory;
        private Mock<ISynchronizationPolicy> synchronizationPolicy;
        private Mock<IProducer<object>> producer;
        private Mock<IServiceScope> scope;
        private Mock<ISynchronizationLock> synchronizationLock;

        [SetUp]
        public void Setup()
        {
            producer = new Mock<IProducer<object>>();

            scope = new Mock<IServiceScope>();
            scopeFactory = new Mock<IServiceScopeFactory>();
            scopeFactory.Setup(o => o.CreateScope()).Returns(scope.Object);

            synchronizationLock = new Mock<ISynchronizationLock>();
            synchronizationPolicy = new Mock<ISynchronizationPolicy>();
            synchronizationPolicy.Setup(o => o.AcquireLockAsync(It.IsAny<CancellationToken>())).ReturnsAsync(synchronizationLock.Object);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheCallbackIsNull()
        {
            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, (scope) => producer.Object, synchronizationPolicy.Object, true);
            Assert.ThrowsAsync<ArgumentNullException>(() => target.ExecuteAsync(null, CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheProducerIsNull()
        {
            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, (scope) => null, synchronizationPolicy.Object, true);
            Assert.ThrowsAsync<RuntimeException>(() => target.ExecuteAsync(context => { }, CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheScopeFactoryReturnsNull()
        {
            scopeFactory.Setup(o => o.CreateScope()).Returns((IServiceScope)null).Verifiable();

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, (scope) => producer.Object, synchronizationPolicy.Object, true);
            Assert.ThrowsAsync<RuntimeException>(() => target.ExecuteAsync(context => { }, CancellationToken.None));

            scopeFactory.Verify();
        }

        [Test]
        public async Task ShouldExecuteTheCallbackWhenNotNull()
        {
            var called = false;

            var item = new object();
            producer.Setup(o => o.ProduceAsync(It.IsAny<CancellationToken>())).ReturnsAsync(item);

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, (scope) => producer.Object, synchronizationPolicy.Object, true);
            await target.ExecuteAsync(context =>
            {
                AssertContext(item, target, context);
                called = true;
            }, CancellationToken.None);

            Assert.IsTrue(called);
        }

        [Test]
        public async Task ShouldExecuteTheCallbackWhenNull()
        {
            var called = false;

            producer.Setup(o => o.ProduceAsync(It.IsAny<CancellationToken>())).ReturnsAsync((object)null);

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, (scope) => producer.Object, synchronizationPolicy.Object, true);
            await target.ExecuteAsync(context =>
            {
                AssertContext(null, target, context);
                called = true;
            }, CancellationToken.None);

            Assert.IsTrue(called);
        }

        [Test]
        public async Task ShouldNotExecuteTheCallbackWhenNull()
        {
            var called = false;

            producer.Setup(o => o.ProduceAsync(It.IsAny<CancellationToken>())).ReturnsAsync((object)null);

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, (scope) => producer.Object, synchronizationPolicy.Object, false);
            await target.ExecuteAsync(context =>
            {
                called = true;
            }, CancellationToken.None);

            Assert.IsFalse(called);
        }

        private void AssertContext(object item, DefaultProducerExecutionStrategy<object> target, ProducerConsumerContext<object> context)
        {
            Assert.AreEqual(item, context.Item);
            Assert.AreEqual(producer.Object, context.Producer);
            Assert.AreEqual(scope.Object, context.LifetimeScope);
            Assert.AreEqual(target, context.ProductionStrategy);
            Assert.AreEqual(synchronizationLock.Object, context.SynchronizationLock);
        }
    }
}
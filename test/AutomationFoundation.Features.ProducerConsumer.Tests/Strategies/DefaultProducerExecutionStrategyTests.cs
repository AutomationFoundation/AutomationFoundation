using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Strategies
{
    [TestFixture]
    public class DefaultProducerExecutionStrategyTests
    {
        private Mock<IServiceScopeFactory> scopeFactory;
        private Mock<ISynchronizationPolicy> synchronizationPolicy;
        private Mock<IProducer<object>> producer;
        private Mock<IProducerFactory<object>> producerFactory;
        private Mock<IServiceScope> scope;
        private Mock<ISynchronizationLock> synchronizationLock;

        [SetUp]
        public void Setup()
        {
            scope = new Mock<IServiceScope>();
            scopeFactory = new Mock<IServiceScopeFactory>();
            scopeFactory.Setup(o => o.CreateScope()).Returns(scope.Object);

            producer = new Mock<IProducer<object>>();
            producerFactory = new Mock<IProducerFactory<object>>();
            producerFactory.Setup(o => o.Create(scope.Object)).Returns(producer.Object);

            synchronizationLock = new Mock<ISynchronizationLock>();
            synchronizationPolicy = new Mock<ISynchronizationPolicy>();
            synchronizationPolicy.Setup(o => o.AcquireLockAsync(It.IsAny<CancellationToken>())).ReturnsAsync(synchronizationLock.Object);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheCallbackIsNull()
        {
            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, producerFactory.Object, synchronizationPolicy.Object, true);
            Assert.ThrowsAsync<ArgumentNullException>(() => target.ExecuteAsync(null, CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheProducerIsNull()
        {
            producerFactory.Setup(o => o.Create(scope.Object)).Returns((IProducer<object>)null);

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, producerFactory.Object, synchronizationPolicy.Object, true);
            Assert.ThrowsAsync<RuntimeException>(() => target.ExecuteAsync(context => { }, CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheScopeFactoryReturnsNull()
        {
            scopeFactory.Setup(o => o.CreateScope()).Returns((IServiceScope)null).Verifiable();

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, producerFactory.Object, synchronizationPolicy.Object, true);
            Assert.ThrowsAsync<RuntimeException>(() => target.ExecuteAsync(context => { }, CancellationToken.None));

            scopeFactory.Verify();
        }

        [Test]
        public async Task ShouldExecuteTheCallbackWhenNotNull()
        {
            var called = false;

            var item = new object();
            producer.Setup(o => o.ProduceAsync(It.IsAny<CancellationToken>())).ReturnsAsync(item);

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, producerFactory.Object, synchronizationPolicy.Object, true);
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

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, producerFactory.Object, synchronizationPolicy.Object, true);
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

            var target = new DefaultProducerExecutionStrategy<object>(scopeFactory.Object, producerFactory.Object, synchronizationPolicy.Object, false);
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
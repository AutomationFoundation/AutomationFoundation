using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Factories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests.Factories
{
    [TestFixture]
    public class SingletonProducerFactoryTests
    {
        private Mock<IProducer<object>> producer;
        private Mock<IServiceScope> scope;

        [SetUp]
        public void Setup()
        {
            producer = new Mock<IProducer<object>>();
            scope = new Mock<IServiceScope>();
        }

        [Test]
        public void ThrowAnExceptionWhenConsumerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SingletonProducerFactory<object>(null));
        }

        [Test]
        public void ThrowsAnExceptionWhenScopeIsNull()
        {
            var target = new SingletonProducerFactory<object>(producer.Object);
            Assert.Throws<ArgumentNullException>(() => target.Create(null));
        }

        [Test]
        public void AlwaysReturnsTheSameInstance()
        {
            var target = new SingletonProducerFactory<object>(producer.Object);

            var result1 = target.Create(scope.Object);
            var result2 = target.Create(scope.Object);

            Assert.AreSame(result1, result2);
        }
    }
}
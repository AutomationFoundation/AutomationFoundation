using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Factories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests.Factories
{
    [TestFixture]
    public class SingletonConsumerFactoryTests
    {
        private Mock<IConsumer<object>> consumer;
        private Mock<IServiceScope> scope;

        [SetUp]
        public void Setup()
        {
            consumer = new Mock<IConsumer<object>>();
            scope = new Mock<IServiceScope>();
        }

        [Test]
        public void ThrowAnExceptionWhenConsumerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SingletonConsumerFactory<object>(null));
        }

        [Test]
        public void ThrowsAnExceptionWhenScopeIsNull()
        {
            var target = new SingletonConsumerFactory<object>(consumer.Object);
            Assert.Throws<ArgumentNullException>(() => target.Create(null));
        }

        [Test]
        public void AlwaysReturnsTheSameInstance()
        {
            var target = new SingletonConsumerFactory<object>(consumer.Object);

            var result1 = target.Create(scope.Object);
            var result2 = target.Create(scope.Object);

            Assert.AreSame(result1, result2);
        }
    }
}
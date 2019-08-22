using AutomationFoundation.Features.ProducerConsumer.Factories;
using AutomationFoundation.Features.ProducerConsumer.Tests.Factories.Stubs;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Tests.Factories
{
    [TestFixture]
    public class SingletonProducerFactoryTests
    {
        private Mock<IServiceScope> scope;

        [SetUp]
        public void Setup()
        {
            scope = new Mock<IServiceScope>();
        }

        [Test]
        public void AlwaysReturnsANewInstance()
        {
            var target = new DefaultProducerFactory<StubProducer, object>();

            var result1 = target.Create(scope.Object);
            var result2 = target.Create(scope.Object);

            Assert.AreNotSame(result1, result2);
        }
    }
}
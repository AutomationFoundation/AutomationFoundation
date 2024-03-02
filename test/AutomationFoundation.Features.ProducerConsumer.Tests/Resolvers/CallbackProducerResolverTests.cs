using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Resolvers.TestObjects;
using Moq;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Features.ProducerConsumer.Tests(net472)'
Before:
namespace AutomationFoundation.Features.ProducerConsumer.Resolvers
{
    [TestFixture]
    public class CallbackProducerResolverTests
    {
        [Test]
        public void ThrowsAnExceptionWhenTheContextIsNull()
        {
            var target = new CallbackProducerResolver<object>((context) => null);
            Assert.Throws<ArgumentNullException>(() => target.Resolve(null));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheCallbackIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CallbackProducerResolver<object>(null));
        }

        [Test]
        public void ReturnsTheResultFromTheCallback()
        {
            var target = new CallbackProducerResolver<object>(_ => new StubProducer());
            var result = target.Resolve(new Mock<IProducerConsumerContext<object>>().Object);

            Assert.IsInstanceOf<StubProducer>(result);
        }

        [Test]
        public void DoesNotThrowAnErrorWhenTheResultIsNull()
        {
            var target = new CallbackProducerResolver<object>(_ => null);
            var result = target.Resolve(new Mock<IProducerConsumerContext<object>>().Object);

            Assert.IsNull(result);
        }
After:
namespace AutomationFoundation.Features.ProducerConsumer.Resolvers;

[TestFixture]
public class CallbackProducerResolverTests
{
    [Test]
    public void ThrowsAnExceptionWhenTheContextIsNull()
    {
        var target = new CallbackProducerResolver<object>((context) => null);
        Assert.Throws<ArgumentNullException>(() => target.Resolve(null));
    }

    [Test]
    public void ThrowsAnExceptionWhenTheCallbackIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new CallbackProducerResolver<object>(null));
    }

    [Test]
    public void ReturnsTheResultFromTheCallback()
    {
        var target = new CallbackProducerResolver<object>(_ => new StubProducer());
        var result = target.Resolve(new Mock<IProducerConsumerContext<object>>().Object);

        Assert.IsInstanceOf<StubProducer>(result);
    }

    [Test]
    public void DoesNotThrowAnErrorWhenTheResultIsNull()
    {
        var target = new CallbackProducerResolver<object>(_ => null);
        var result = target.Resolve(new Mock<IProducerConsumerContext<object>>().Object);

        Assert.IsNull(result);
*/

namespace AutomationFoundation.Features.ProducerConsumer.Resolvers;

[TestFixture]
public class CallbackProducerResolverTests
{
    [Test]
    public void ThrowsAnExceptionWhenTheContextIsNull()
    {
        var target = new CallbackProducerResolver<object>((context) => null);
        Assert.Throws<ArgumentNullException>(() => target.Resolve(null));
    }

    [Test]
    public void ThrowsAnExceptionWhenTheCallbackIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new CallbackProducerResolver<object>(null));
    }

    [Test]
    public void ReturnsTheResultFromTheCallback()
    {
        var target = new CallbackProducerResolver<object>(_ => new StubProducer());
        var result = target.Resolve(new Mock<IProducerConsumerContext<object>>().Object);

        Assert.IsInstanceOf<StubProducer>(result);
    }

    [Test]
    public void DoesNotThrowAnErrorWhenTheResultIsNull()
    {
        var target = new CallbackProducerResolver<object>(_ => null);
        var result = target.Resolve(new Mock<IProducerConsumerContext<object>>().Object);

        Assert.IsNull(result);
    }
}
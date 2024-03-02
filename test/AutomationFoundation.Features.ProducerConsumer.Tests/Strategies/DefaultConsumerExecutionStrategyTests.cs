using System;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Strategies.TestObjects;
using AutomationFoundation.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Features.ProducerConsumer.Tests(net472)'
Before:
namespace AutomationFoundation.Features.ProducerConsumer.Strategies
{
    [TestFixture]
    public class DefaultConsumerExecutionStrategyTests
    {
        private Mock<IServiceScope> lifetimeScope;
        private ProducerConsumerContext<object> context;
        private Mock<IConsumer<object>> consumer;
        private Mock<IConsumerResolver<object>> resolver;

        [SetUp]
        public void Setup()
        {
            lifetimeScope = new Mock<IServiceScope>();
            context = new ProducerConsumerContext<object>(Guid.NewGuid(), lifetimeScope.Object);

            consumer = new Mock<IConsumer<object>>();
            resolver = new Mock<IConsumerResolver<object>>();
            resolver.Setup(o => o.Resolve(context)).Returns(consumer.Object);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheConsumerFactoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultConsumerExecutionStrategy<object>(null));
        }

        [Test]
        public void ThrowsExceptionWhenContextIsNull()
        {
            var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);
            
            Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(null));
        }

        [Test]
        public void ThrowsExceptionWhenContextIsNull1()
        {
            var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);
            target.SetOverrideContext(null);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(context));
        }

        [Test]
        public void ThrowsExceptionWhenContextIsNull2()
        {
            var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object, (strategy, context) =>
            {
                strategy.SetOverrideContext(null);
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(context));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheConsumerIsNull()
        {
            resolver.Setup(o => o.Resolve(context)).Returns((IConsumer<object>)null);

            var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(context));
        }

        [Test]
        public void ReleasesTheProcessingContextWhenFailsToCreateConsumer()
        {
            resolver.Setup(o => o.Resolve(context)).Throws<InvalidOperationException>();

            var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object, onExitCallback: (strategy, context) => {
                Assert.IsNull(ProcessingContext.Current);
            });

            Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(context));
        }

        [Test]
        public async Task ExecutesAllExtensionMethodsInOrderAsExpected()
        {
            bool called1 = false, called2 = false, called3 = false, called4 = false;

            var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object,
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
After:
namespace AutomationFoundation.Features.ProducerConsumer.Strategies;

[TestFixture]
public class DefaultConsumerExecutionStrategyTests
{
    private Mock<IServiceScope> lifetimeScope;
    private ProducerConsumerContext<object> context;
    private Mock<IConsumer<object>> consumer;
    private Mock<IConsumerResolver<object>> resolver;

    [SetUp]
    public void Setup()
    {
        lifetimeScope = new Mock<IServiceScope>();
        context = new ProducerConsumerContext<object>(Guid.NewGuid(), lifetimeScope.Object);

        consumer = new Mock<IConsumer<object>>();
        resolver = new Mock<IConsumerResolver<object>>();
        resolver.Setup(o => o.Resolve(context)).Returns(consumer.Object);
    }

    [Test]
    public void ThrowsAnExceptionWhenTheConsumerFactoryIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new DefaultConsumerExecutionStrategy<object>(null));
    }

    [Test]
    public void ThrowsExceptionWhenContextIsNull()
    {
        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);
        
        Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(null));
    }

    [Test]
    public void ThrowsExceptionWhenContextIsNull1()
    {
        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);
        target.SetOverrideContext(null);

        Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(context));
    }

    [Test]
    public void ThrowsExceptionWhenContextIsNull2()
    {
        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object, (strategy, context) =>
        {
            strategy.SetOverrideContext(null);
        });

        Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(context));
    }

    [Test]
    public void ThrowsAnExceptionWhenTheConsumerIsNull()
    {
        resolver.Setup(o => o.Resolve(context)).Returns((IConsumer<object>)null);

        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);

        Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(context));
    }

    [Test]
    public void ReleasesTheProcessingContextWhenFailsToCreateConsumer()
    {
        resolver.Setup(o => o.Resolve(context)).Throws<InvalidOperationException>();

        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object, onExitCallback: (strategy, context) => {
            Assert.IsNull(ProcessingContext.Current);
        });

        Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(context));
    }

    [Test]
    public async Task ExecutesAllExtensionMethodsInOrderAsExpected()
    {
        bool called1 = false, called2 = false, called3 = false, called4 = false;

        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object,
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
*/

namespace AutomationFoundation.Features.ProducerConsumer.Strategies;

[TestFixture]
public class DefaultConsumerExecutionStrategyTests
{
    private Mock<IServiceScope> lifetimeScope;
    private ProducerConsumerContext<object> context;
    private Mock<IConsumer<object>> consumer;
    private Mock<IConsumerResolver<object>> resolver;

    [SetUp]
    public void Setup()
    {
        lifetimeScope = new Mock<IServiceScope>();
        context = new ProducerConsumerContext<object>(Guid.NewGuid(), lifetimeScope.Object);

        consumer = new Mock<IConsumer<object>>();
        resolver = new Mock<IConsumerResolver<object>>();
        resolver.Setup(o => o.Resolve(context)).Returns(consumer.Object);
    }

    [Test]
    public void ThrowsAnExceptionWhenTheConsumerFactoryIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new DefaultConsumerExecutionStrategy<object>(null));
    }

    [Test]
    public void ThrowsExceptionWhenContextIsNull()
    {
        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);

        Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(null));
    }

    [Test]
    public void ThrowsExceptionWhenContextIsNull1()
    {
        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);
        target.SetOverrideContext(null);

        Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(context));
    }

    [Test]
    public void ThrowsExceptionWhenContextIsNull2()
    {
        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object, (strategy, context) =>
        {
            strategy.SetOverrideContext(null);
        });

        Assert.ThrowsAsync<ArgumentNullException>(async () => await target.ExecuteAsync(context));
    }

    [Test]
    public void ThrowsAnExceptionWhenTheConsumerIsNull()
    {
        resolver.Setup(o => o.Resolve(context)).Returns((IConsumer<object>)null);

        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object);

        Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(context));
    }

    [Test]
    public void ReleasesTheProcessingContextWhenFailsToCreateConsumer()
    {
        resolver.Setup(o => o.Resolve(context)).Throws<InvalidOperationException>();

        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object, onExitCallback: (strategy, context) =>
        {
            Assert.IsNull(ProcessingContext.Current);
        });

        Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(context));
    }

    [Test]
    public async Task ExecutesAllExtensionMethodsInOrderAsExpected()
    {
        bool called1 = false, called2 = false, called3 = false, called4 = false;

        var target = new StubDefaultConsumerExecutionStrategy<object>(resolver.Object,
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
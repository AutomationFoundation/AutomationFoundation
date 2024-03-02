using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Runtime.Tests(net472)'
Before:
namespace AutomationFoundation.Runtime
{
    [TestFixture]
    public class CompositeErrorHandlingStrategyTests
    {
        [Test]
        public void ThrowAnExceptionForDuplicateStrategies1()
        {
            var strategy = new Mock<IErrorHandlingStrategy>();

            Assert.Throws<ArgumentException>(() => CompositeErrorHandlingStrategy.Create(strategy.Object, strategy.Object));
        }

        [Test]
        public void ThrowAnExceptionForDuplicateStrategies2()
        {
            var strategy = new Mock<IErrorHandlingStrategy>();
            IEnumerable<IErrorHandlingStrategy> strategies = new[]
            {
                strategy.Object,
                strategy.Object
            };

            Assert.Throws<ArgumentException>(() => CompositeErrorHandlingStrategy.Create(strategies));
        }

        [Test]
        public void ExecutesAllStrategiesInTheOriginalOrder()
        {
            var counter = 0;
            var context = new ErrorHandlingContext(this, ErrorSeverityLevel.NonFatal, new Exception());

            var strategy1 = new Mock<IErrorHandlingStrategy>();
            strategy1.Setup(o => o.Handle(context)).Callback(() =>
            {
                Assert.AreEqual(0, counter);
                counter++;
            });

            var strategy2 = new Mock<IErrorHandlingStrategy>();
            strategy2.Setup(o => o.Handle(context)).Callback(() =>
            {
                Assert.AreEqual(1, counter);
                counter++;
            });

            var target = new CompositeErrorHandlingStrategy();
            target.AddStrategy(strategy1.Object);
            target.AddStrategy(strategy2.Object);

            target.Handle(context);

            Assert.AreEqual(2, counter);
        }

        [Test]
        public void ContinueCheckingStrategiesAfterBeingHandled()
        {
            var counter = 0;
            var context = new ErrorHandlingContext(this, ErrorSeverityLevel.NonFatal, new Exception());

            var strategy1 = new Mock<IErrorHandlingStrategy>();
            strategy1.Setup(o => o.Handle(context)).Callback(() =>
            {
                context.MarkErrorAsHandled();
                counter++;
            });

            var strategy2 = new Mock<IErrorHandlingStrategy>();
            strategy2.Setup(o => o.Handle(context)).Callback(() =>
            {
                counter++;
            });

            var target = new CompositeErrorHandlingStrategy();
            target.AddStrategy(strategy1.Object);
            target.AddStrategy(strategy2.Object);

            target.Handle(context);

            Assert.AreEqual(2, counter);
        }

        [Test]
        public void ThrowAnExceptionWhenAddingANullStrategy()
        {
            var target = new CompositeErrorHandlingStrategy();
            Assert.Throws<ArgumentNullException>(() => target.AddStrategy(null));
        }

        [Test]
        public void ThrowAnExceptionWhenAddingADuplicateStrategy()
        {
            var strategy = new Mock<IErrorHandlingStrategy>();

            var target = new CompositeErrorHandlingStrategy();
            target.AddStrategy(strategy.Object);

            Assert.Throws<ArgumentException>(() => target.AddStrategy(strategy.Object));
        }

        [Test]
        public void ThrowAnExceptionWhenRemovingANullStrategy()
        {
            var target = new CompositeErrorHandlingStrategy();
            Assert.Throws<ArgumentNullException>(() => target.RemoveStrategy(null));
        }

        [Test]
        public void ThrowAnExceptionWhenRemovingAStrategyThatDoesNotExist()
        {
            var strategy = new Mock<IErrorHandlingStrategy>();

            var target = new CompositeErrorHandlingStrategy();
            Assert.Throws<ArgumentException>(() => target.RemoveStrategy(strategy.Object));
        }
After:
namespace AutomationFoundation.Runtime;

[TestFixture]
public class CompositeErrorHandlingStrategyTests
{
    [Test]
    public void ThrowAnExceptionForDuplicateStrategies1()
    {
        var strategy = new Mock<IErrorHandlingStrategy>();

        Assert.Throws<ArgumentException>(() => CompositeErrorHandlingStrategy.Create(strategy.Object, strategy.Object));
    }

    [Test]
    public void ThrowAnExceptionForDuplicateStrategies2()
    {
        var strategy = new Mock<IErrorHandlingStrategy>();
        IEnumerable<IErrorHandlingStrategy> strategies = new[]
        {
            strategy.Object,
            strategy.Object
        };

        Assert.Throws<ArgumentException>(() => CompositeErrorHandlingStrategy.Create(strategies));
    }

    [Test]
    public void ExecutesAllStrategiesInTheOriginalOrder()
    {
        var counter = 0;
        var context = new ErrorHandlingContext(this, ErrorSeverityLevel.NonFatal, new Exception());

        var strategy1 = new Mock<IErrorHandlingStrategy>();
        strategy1.Setup(o => o.Handle(context)).Callback(() =>
        {
            Assert.AreEqual(0, counter);
            counter++;
        });

        var strategy2 = new Mock<IErrorHandlingStrategy>();
        strategy2.Setup(o => o.Handle(context)).Callback(() =>
        {
            Assert.AreEqual(1, counter);
            counter++;
        });

        var target = new CompositeErrorHandlingStrategy();
        target.AddStrategy(strategy1.Object);
        target.AddStrategy(strategy2.Object);

        target.Handle(context);

        Assert.AreEqual(2, counter);
    }

    [Test]
    public void ContinueCheckingStrategiesAfterBeingHandled()
    {
        var counter = 0;
        var context = new ErrorHandlingContext(this, ErrorSeverityLevel.NonFatal, new Exception());

        var strategy1 = new Mock<IErrorHandlingStrategy>();
        strategy1.Setup(o => o.Handle(context)).Callback(() =>
        {
            context.MarkErrorAsHandled();
            counter++;
        });

        var strategy2 = new Mock<IErrorHandlingStrategy>();
        strategy2.Setup(o => o.Handle(context)).Callback(() =>
        {
            counter++;
        });

        var target = new CompositeErrorHandlingStrategy();
        target.AddStrategy(strategy1.Object);
        target.AddStrategy(strategy2.Object);

        target.Handle(context);

        Assert.AreEqual(2, counter);
    }

    [Test]
    public void ThrowAnExceptionWhenAddingANullStrategy()
    {
        var target = new CompositeErrorHandlingStrategy();
        Assert.Throws<ArgumentNullException>(() => target.AddStrategy(null));
    }

    [Test]
    public void ThrowAnExceptionWhenAddingADuplicateStrategy()
    {
        var strategy = new Mock<IErrorHandlingStrategy>();

        var target = new CompositeErrorHandlingStrategy();
        target.AddStrategy(strategy.Object);

        Assert.Throws<ArgumentException>(() => target.AddStrategy(strategy.Object));
    }

    [Test]
    public void ThrowAnExceptionWhenRemovingANullStrategy()
    {
        var target = new CompositeErrorHandlingStrategy();
        Assert.Throws<ArgumentNullException>(() => target.RemoveStrategy(null));
    }

    [Test]
    public void ThrowAnExceptionWhenRemovingAStrategyThatDoesNotExist()
    {
        var strategy = new Mock<IErrorHandlingStrategy>();

        var target = new CompositeErrorHandlingStrategy();
        Assert.Throws<ArgumentException>(() => target.RemoveStrategy(strategy.Object));
*/

namespace AutomationFoundation.Runtime;

[TestFixture]
public class CompositeErrorHandlingStrategyTests
{
    [Test]
    public void ThrowAnExceptionForDuplicateStrategies1()
    {
        var strategy = new Mock<IErrorHandlingStrategy>();

        Assert.Throws<ArgumentException>(() => CompositeErrorHandlingStrategy.Create(strategy.Object, strategy.Object));
    }

    [Test]
    public void ThrowAnExceptionForDuplicateStrategies2()
    {
        var strategy = new Mock<IErrorHandlingStrategy>();
        IEnumerable<IErrorHandlingStrategy> strategies = new[]
        {
            strategy.Object,
            strategy.Object
        };

        Assert.Throws<ArgumentException>(() => CompositeErrorHandlingStrategy.Create(strategies));
    }

    [Test]
    public void ExecutesAllStrategiesInTheOriginalOrder()
    {
        var counter = 0;
        var context = new ErrorHandlingContext(this, ErrorSeverityLevel.NonFatal, new Exception());

        var strategy1 = new Mock<IErrorHandlingStrategy>();
        strategy1.Setup(o => o.Handle(context)).Callback(() =>
        {
            Assert.AreEqual(0, counter);
            counter++;
        });

        var strategy2 = new Mock<IErrorHandlingStrategy>();
        strategy2.Setup(o => o.Handle(context)).Callback(() =>
        {
            Assert.AreEqual(1, counter);
            counter++;
        });

        var target = new CompositeErrorHandlingStrategy();
        target.AddStrategy(strategy1.Object);
        target.AddStrategy(strategy2.Object);

        target.Handle(context);

        Assert.AreEqual(2, counter);
    }

    [Test]
    public void ContinueCheckingStrategiesAfterBeingHandled()
    {
        var counter = 0;
        var context = new ErrorHandlingContext(this, ErrorSeverityLevel.NonFatal, new Exception());

        var strategy1 = new Mock<IErrorHandlingStrategy>();
        strategy1.Setup(o => o.Handle(context)).Callback(() =>
        {
            context.MarkErrorAsHandled();
            counter++;
        });

        var strategy2 = new Mock<IErrorHandlingStrategy>();
        strategy2.Setup(o => o.Handle(context)).Callback(() =>
        {
            counter++;
        });

        var target = new CompositeErrorHandlingStrategy();
        target.AddStrategy(strategy1.Object);
        target.AddStrategy(strategy2.Object);

        target.Handle(context);

        Assert.AreEqual(2, counter);
    }

    [Test]
    public void ThrowAnExceptionWhenAddingANullStrategy()
    {
        var target = new CompositeErrorHandlingStrategy();
        Assert.Throws<ArgumentNullException>(() => target.AddStrategy(null));
    }

    [Test]
    public void ThrowAnExceptionWhenAddingADuplicateStrategy()
    {
        var strategy = new Mock<IErrorHandlingStrategy>();

        var target = new CompositeErrorHandlingStrategy();
        target.AddStrategy(strategy.Object);

        Assert.Throws<ArgumentException>(() => target.AddStrategy(strategy.Object));
    }

    [Test]
    public void ThrowAnExceptionWhenRemovingANullStrategy()
    {
        var target = new CompositeErrorHandlingStrategy();
        Assert.Throws<ArgumentNullException>(() => target.RemoveStrategy(null));
    }

    [Test]
    public void ThrowAnExceptionWhenRemovingAStrategyThatDoesNotExist()
    {
        var strategy = new Mock<IErrorHandlingStrategy>();

        var target = new CompositeErrorHandlingStrategy();
        Assert.Throws<ArgumentException>(() => target.RemoveStrategy(strategy.Object));
    }
}
using System;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Runtime;

[TestFixture]
public class StrategyErrorHandlerTests
{
    [Test]
    public void DoesNotRethrowTheExceptionAfterItHasBeenHandled()
    {
        var errorHandlingStrategy = new Mock<IErrorHandlingStrategy>();
        errorHandlingStrategy.Setup(o => o.Handle(It.IsAny<ErrorHandlingContext>())).Callback<ErrorHandlingContext>(ctx => ctx.MarkErrorAsHandled());

        var target = new StrategyErrorHandler(errorHandlingStrategy.Object);
        Assert.DoesNotThrow(() => target.Handle(new Exception(), ErrorSeverityLevel.NonFatal));

        errorHandlingStrategy.Verify(o => o.Handle(It.IsAny<ErrorHandlingContext>()), Times.Once);
    }

    [Test]
    public void RethrowsTheExceptionWhenItHasNotBeenHandled()
    {
        var errorHandlingStrategy = new Mock<IErrorHandlingStrategy>();

        var target = new StrategyErrorHandler(errorHandlingStrategy.Object);
        Assert.Throws<Exception>(() => target.Handle(new Exception(), ErrorSeverityLevel.NonFatal));

        errorHandlingStrategy.Verify(o => o.Handle(It.IsAny<ErrorHandlingContext>()), Times.Once);
    }

    [Test]
    public void ThrowAnExceptionWhenTheErrorIsNull()
    {
        var errorHandlingStrategy = new Mock<IErrorHandlingStrategy>();

        var target = new StrategyErrorHandler(errorHandlingStrategy.Object);
        Assert.Throws<ArgumentNullException>(() => target.Handle(null, ErrorSeverityLevel.NonFatal));
    }
}
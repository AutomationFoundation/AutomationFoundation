using System;
using System.Threading;
using AutomationFoundation.Features.ProducerConsumer.Abstractions.TestObjects;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using Moq;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Features.ProducerConsumer.Tests(net472)'
Before:
namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    [TestFixture]
    public class ProducerEngineTests
    {
        private Mock<ICancellationSourceFactory> cancellationSourceFactory;
        private Mock<ICancellationSource> cancellationSource;

        [SetUp]
        public void Setup()
        {
            cancellationSourceFactory = new Mock<ICancellationSourceFactory>();
            cancellationSource = new Mock<ICancellationSource>();
        }

        [Test]
        public void ThrowsAnExceptionWhenCallbackIsNull()
        {
            using var target = new StubProducerEngine(cancellationSourceFactory.Object);
            Assert.Throws<ArgumentNullException>(() => target.Initialize(null, CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenCancellationSourceIsNull()
        {
            using var target = new StubProducerEngine(cancellationSourceFactory.Object);
            Assert.Throws<InvalidOperationException>(() => target.Initialize(context => { }, CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenStartedBeforeInitialized()
        {
            cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

            using var target = new StubProducerEngine(cancellationSourceFactory.Object);
            Assert.ThrowsAsync<EngineException>(async () => await target.StartAsync());
        }


        [Test]
        public void ThrowsAnExceptionWhenInitializedTwice()
        {
            cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

            using var target = new StubProducerEngine(cancellationSourceFactory.Object);
            target.Initialize(context => { }, CancellationToken.None);

            Assert.Throws<EngineException>(() => target.Initialize(context => { }, CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenWaitingBeforeStarted()
        {
            cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

            using var target = new StubProducerEngine(cancellationSourceFactory.Object);
            target.Initialize(context => { }, CancellationToken.None);

            Assert.ThrowsAsync<EngineException>(async () => await target.WaitForCompletionAsync());
        }
After:
namespace AutomationFoundation.Features.ProducerConsumer.Abstractions;

[TestFixture]
public class ProducerEngineTests
{
    private Mock<ICancellationSourceFactory> cancellationSourceFactory;
    private Mock<ICancellationSource> cancellationSource;

    [SetUp]
    public void Setup()
    {
        cancellationSourceFactory = new Mock<ICancellationSourceFactory>();
        cancellationSource = new Mock<ICancellationSource>();
    }

    [Test]
    public void ThrowsAnExceptionWhenCallbackIsNull()
    {
        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        Assert.Throws<ArgumentNullException>(() => target.Initialize(null, CancellationToken.None));
    }

    [Test]
    public void ThrowsAnExceptionWhenCancellationSourceIsNull()
    {
        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        Assert.Throws<InvalidOperationException>(() => target.Initialize(context => { }, CancellationToken.None));
    }

    [Test]
    public void ThrowsAnExceptionWhenStartedBeforeInitialized()
    {
        cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        Assert.ThrowsAsync<EngineException>(async () => await target.StartAsync());
    }


    [Test]
    public void ThrowsAnExceptionWhenInitializedTwice()
    {
        cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        target.Initialize(context => { }, CancellationToken.None);

        Assert.Throws<EngineException>(() => target.Initialize(context => { }, CancellationToken.None));
    }

    [Test]
    public void ThrowsAnExceptionWhenWaitingBeforeStarted()
    {
        cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        target.Initialize(context => { }, CancellationToken.None);

        Assert.ThrowsAsync<EngineException>(async () => await target.WaitForCompletionAsync());
*/

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions;

[TestFixture]
public class ProducerEngineTests
{
    private Mock<ICancellationSourceFactory> cancellationSourceFactory;
    private Mock<ICancellationSource> cancellationSource;

    [SetUp]
    public void Setup()
    {
        cancellationSourceFactory = new Mock<ICancellationSourceFactory>();
        cancellationSource = new Mock<ICancellationSource>();
    }

    [Test]
    public void ThrowsAnExceptionWhenCallbackIsNull()
    {
        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        Assert.Throws<ArgumentNullException>(() => target.Initialize(null, CancellationToken.None));
    }

    [Test]
    public void ThrowsAnExceptionWhenCancellationSourceIsNull()
    {
        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        Assert.Throws<InvalidOperationException>(() => target.Initialize(context => { }, CancellationToken.None));
    }

    [Test]
    public void ThrowsAnExceptionWhenStartedBeforeInitialized()
    {
        cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        Assert.ThrowsAsync<EngineException>(async () => await target.StartAsync());
    }


    [Test]
    public void ThrowsAnExceptionWhenInitializedTwice()
    {
        cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        target.Initialize(context => { }, CancellationToken.None);

        Assert.Throws<EngineException>(() => target.Initialize(context => { }, CancellationToken.None));
    }

    [Test]
    public void ThrowsAnExceptionWhenWaitingBeforeStarted()
    {
        cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource.Object);

        using var target = new StubProducerEngine(cancellationSourceFactory.Object);
        target.Initialize(context => { }, CancellationToken.None);

        Assert.ThrowsAsync<EngineException>(async () => await target.WaitForCompletionAsync());
    }
}
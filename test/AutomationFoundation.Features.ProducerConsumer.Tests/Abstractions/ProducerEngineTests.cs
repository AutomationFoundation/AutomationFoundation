using System;
using System.Threading;
using AutomationFoundation.Features.ProducerConsumer.Abstractions.Stubs;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    [TestFixture]
    public class ProducerEngineTests
    {
        private Mock<ICancellationSourceFactory> cancellationSourceFactory;

        [SetUp]
        public void Setup()
        {
            cancellationSourceFactory = new Mock<ICancellationSourceFactory>();
        }

        [Test]
        public void ThrowsAnExceptionWhenCallbackIsNull()
        {
            using (var target = new StubProducerEngine(cancellationSourceFactory.Object))
            {
                Assert.Throws<ArgumentNullException>(() => target.Initialize(null, CancellationToken.None));
            }
        }

        [Test]
        public void ThrowsAnExceptionWhenCancellationSourceIsNull()
        {
            using (var target = new StubProducerEngine(cancellationSourceFactory.Object))
            {
                Assert.Throws<InvalidOperationException>(() => target.Initialize(context => { }, CancellationToken.None));
            }
        }
    }
}
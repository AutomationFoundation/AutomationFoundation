using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Engines.Configuration;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using AutomationFoundation.Runtime.Threading.Primitives;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Features.ProducerConsumer.Engines
{
    [TestFixture]
    public class ScheduledProducerEngineTests
    {
        private Mock<IProducerExecutionStrategy<object>> executionStrategy;
        private Mock<ICancellationSourceFactory> cancellationSourceFactory;
        private Mock<IErrorHandler> errorHandler;
        private Mock<IScheduler> scheduler;

        [SetUp]
        public void Setup()
        {
            executionStrategy = new Mock<IProducerExecutionStrategy<object>>();
            cancellationSourceFactory = new Mock<ICancellationSourceFactory>();
            errorHandler = new Mock<IErrorHandler>();
            scheduler = new Mock<IScheduler>();
        }

        [Test]
        [Timeout(10000)]
        public async Task StartAndStopTheEngineCorrectly()
        {
            using (var cancellationSource = new CancellationSource())
            using (var target = new ScheduledProducerEngine<object>(executionStrategy.Object, cancellationSourceFactory.Object, errorHandler.Object, scheduler.Object, new ScheduledEngineOptions()))
            {
                cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource);

                target.Initialize(context => { }, CancellationToken.None);

                await target.StartAsync();
                Assert.True(target.IsRunning);

                await target.StopAsync();
                Assert.False(target.IsRunning);
            }
        }

        [Test]
        [Timeout(10000)]
        public async Task RunsTheExecutionStrategy()
        {
            executionStrategy.Setup(o => o.ExecuteAsync(It.IsAny<Action<IProducerConsumerContext<object>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            using (var cancellationSource = new CancellationSource())
            using (var target = new ScheduledProducerEngine<object>(executionStrategy.Object, cancellationSourceFactory.Object, errorHandler.Object, scheduler.Object, new ScheduledEngineOptions()))
            {
                cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource);

                target.Initialize(context => { }, CancellationToken.None);

                await target.StartAsync();
                Assert.True(target.IsRunning);

                cancellationSource.RequestCancellationAfter(TimeSpan.FromSeconds(5));

                await target.WaitForCompletionAsync();
            }

            executionStrategy.Verify(o => o.ExecuteAsync(It.IsAny<Action<IProducerConsumerContext<object>>>(), 
                It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }

        [Test]
        [Timeout(10000)]
        public async Task RunsTheErrorHandlerWhenAnErrorHappensInStrategy()
        {
            executionStrategy.Setup(o => o.ExecuteAsync(It.IsAny<Action<IProducerConsumerContext<object>>>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("An exception occurred"));

            using (var cancellationSource = new CancellationSource())
            using (var target = new ScheduledProducerEngine<object>(executionStrategy.Object, cancellationSourceFactory.Object, errorHandler.Object, scheduler.Object, new ScheduledEngineOptions()))
            {
                cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource);

                target.Initialize(context => { }, CancellationToken.None);

                await target.StartAsync();
                Assert.True(target.IsRunning);

                cancellationSource.RequestCancellationAfter(TimeSpan.FromSeconds(5));

                await target.WaitForCompletionAsync();
            }

            errorHandler.Verify(o => o.Handle(It.IsAny<Exception>(), ErrorSeverityLevel.NonFatal), Times.AtLeastOnce);
        }

        [Test]
        [Timeout(10000)]
        public async Task RunsTheExecutionStrategyRepeatedly()
        {
            executionStrategy.Setup(o => o.ExecuteAsync(It.IsAny<Action<IProducerConsumerContext<object>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            using (var cancellationSource = new CancellationSource())
            using (var target = new ScheduledProducerEngine<object>(executionStrategy.Object, cancellationSourceFactory.Object, errorHandler.Object, scheduler.Object, new ScheduledEngineOptions()))
            {
                cancellationSourceFactory.Setup(o => o.Create(It.IsAny<CancellationToken>())).Returns(cancellationSource);

                target.Initialize(context => { }, CancellationToken.None);

                await target.StartAsync();
                Assert.True(target.IsRunning);

                cancellationSource.RequestCancellationAfter(TimeSpan.FromSeconds(5));

                await target.WaitForCompletionAsync();
            }

            executionStrategy.Verify(o => o.ExecuteAsync(It.IsAny<Action<IProducerConsumerContext<object>>>(),
                It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }

        [Test]
        public void DoesNotThrowAnExceptionWhenCancelledWhileDelayed()
        {

        }
    }
}
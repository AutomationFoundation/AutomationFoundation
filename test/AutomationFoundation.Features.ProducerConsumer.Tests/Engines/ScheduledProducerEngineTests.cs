//using System.Threading;
//using System.Threading.Tasks;
//using AutomationFoundation.Features.ProducerConsumer.Abstractions;
//using AutomationFoundation.Features.ProducerConsumer.Configuration;
//using AutomationFoundation.Features.ProducerConsumer.Engines;
//using AutomationFoundation.Runtime.Abstractions;
//using Moq;
//using NUnit.Framework;

//namespace AutomationFoundation.Features.ProducerConsumer.Tests
//{
//    [TestFixture]
//    public class ScheduledProducerEngineTests
//    {
//        [Test]
//        public async Task StartAndStopTheEngineCorrectly()
//        {
//            var options = new ScheduledEngineOptions();

//            var runner = new Mock<IProducerRunner>();
//            var errorHandler = new Mock<IErrorHandler>();
//            var scheduler = new Mock<IScheduler>();

//            var target = new ScheduledProducerEngine(runner.Object, errorHandler.Object, scheduler.Object, options);
//            target.Initialize(CancellationToken.None);

//            await target.StartAsync(new ProducerEngineContext());

//            Assert.IsTrue(target.IsRunning);

//            await target.StopAsync();

//            Assert.IsFalse(target.IsRunning);
//        }
//    }
//}
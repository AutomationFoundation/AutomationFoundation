//using System;
//using System.Collections.Generic;
//using AutomationFoundation.Runtime.Tests.Stubs;
//using AutomationFoundation.Runtime.Threading.Internal;
//using AutomationFoundation.Runtime.Threading.Primitives;
//using Moq;
//using NUnit.Framework;

//namespace AutomationFoundation.Runtime.Tests.Threading.Internal
//{
//    [TestFixture]
//    public class WorkerCacheTests
//    {
//        [Test]
//        public void ThrowsAnExceptionWhenTheAvailableCacheIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(() => new WorkerCache(null, new HashSet<WorkerCacheEntry>()));
//        }

//        [Test]
//        public void ThrowsAnExceptionWhenTheBusyCacheIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(() => new WorkerCache(new HashSet<WorkerCacheEntry>(), null));
//        }

//        [Test]
//        public void GetsTheWorkerAsExpected()
//        {
//            var worker = Mock.Of<Worker>();

//            var entry = new WorkerCacheEntry(worker, DateTime.Now);

//            var available = new HashSet<WorkerCacheEntry>(new[] { entry });
//            var busy = new HashSet<WorkerCacheEntry>();

//            var target = new WorkerCache(available, busy);
//            var result = target.Get();

//            Assert.AreEqual(worker, result);
//            Assert.True(available.Count == 0);
//            Assert.True(busy.Count == 1);
//        }

//        [Test]
//        public void CreatesAWorkerWhenNoneAvailable()
//        {
//            var target = new TestableWorkerCache();
//            var worker = target.Get();

//            Assert.IsNotNull(worker);
//            Assert.True(target.CreatedWorker);
//        }

//        [Test]
//        public void ThrowsAnExceptionWhenTheWorkerCreatedIsNull()
//        {
//            var target = new TestableWorkerCache
//            {
//                OnCreateWorkerCallback = () => null
//            };

//            Assert.Throws<InvalidOperationException>(() => target.Get());
//        }

//        [Test]
//        public void ThrowsAnExceptionWhenTheWorkerIsNull()
//        {
//            var target = new WorkerCache();
//            Assert.Throws<ArgumentNullException>(() => target.Release(null));
//        }

//        [Test]
//        public void MustResetTheWorkerDuringRelease()
//        {
//            var worker = new Mock<Worker>();

//            var entry = new WorkerCacheEntry(worker.Object, DateTime.Now);

//            var target = new WorkerCache(new HashSet<WorkerCacheEntry>(), new HashSet<WorkerCacheEntry>(new[] { entry }));
//            target.Release(worker.Object);

//            worker.Verify(o => o.Reset(), Times.Once);
//        }

//        [Test]
//        public void MustNotThrowAnExceptionWhenTheWorkerIsReleasedMoreThanOnce()
//        {
//            var worker = Mock.Of<Worker>();

//            var entry = new WorkerCacheEntry(worker, DateTime.Now);

//            var available = new HashSet<WorkerCacheEntry>();
//            var busy = new HashSet<WorkerCacheEntry>(new[] { entry });
//            Assert.True(busy.Count == 1);

//            var target = new WorkerCache(available, busy);
//            target.Release(worker);

//            Assert.True(busy.Count == 0);
//            Assert.True(available.Count == 1);

//            Assert.DoesNotThrow(() => target.Release(worker));

//            Assert.True(busy.Count == 0);
//            Assert.True(available.Count == 1);
//        }
//    }
//}
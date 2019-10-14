using System.Threading;
using AutomationFoundation.Runtime.Synchronization.Primitives;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Synchronization
{
    [TestFixture]
    public class SemaphoreLockTests
    {
        [Test]
        [Timeout(2000)]
        public void ShouldReleaseTheLockOnRelease()
        {
            using var semaphore = new SemaphoreSlim(0, 1);
            Assert.AreEqual(0, semaphore.CurrentCount);

            var target = new SemaphoreLock(semaphore);
            target.Release();

            Assert.AreEqual(1, semaphore.CurrentCount);
        }
    }
}
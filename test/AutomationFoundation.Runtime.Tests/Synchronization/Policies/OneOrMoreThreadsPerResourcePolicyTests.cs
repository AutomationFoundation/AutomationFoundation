using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Synchronization.Policies;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Tests.Synchronization.Policies
{
    [TestFixture]
    public class OneOrMoreThreadsPerResourcePolicyTests
    {
        private OneOrMoreThreadsPerResourcePolicy target;
        private CancellationTokenSource cancellationSource;

        [SetUp]
        public void Setup()
        {
            cancellationSource = new CancellationTokenSource();
        }

        [TearDown]
        public void Finish()
        {
            target?.Dispose();
            cancellationSource?.Dispose();
        }

        [Test]
        public void ThrowsAnExceptionWhenMaximumIsLessThanZero()
        {
            Assert.Throws<ArgumentException>(() => new OneOrMoreThreadsPerResourcePolicy(-1));
        }

        [Test]
        public void ThrowsAnExceptionWhenMaximumIsEqualToZero()
        {
            Assert.Throws<ArgumentException>(() => new OneOrMoreThreadsPerResourcePolicy(0));
        }

        [Test]
        [Timeout(2000)]
        public async Task PreventMultipleThreadsFromAccessingTheResource()
        {
            target = new OneOrMoreThreadsPerResourcePolicy(1);

            await target.AcquireLockAsync(CancellationToken.None);

            cancellationSource.CancelAfter(1000);

            Assert.ThrowsAsync<TaskCanceledException>(async () => await target.AcquireLockAsync(cancellationSource.Token));
        }
    }
}
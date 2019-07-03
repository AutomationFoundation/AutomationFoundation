using System;
using System.Threading;
using AutomationFoundation.Runtime.Synchronization.Policies;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Tests.Synchronization.Policies
{
    [TestFixture]
    public class OneOrMoreThreadsPerResourcePolicyTests
    {
        [Test]
        [Timeout(2000)]
        public void PreventMultipleThreadsFromAccessingTheResource()
        {
            using (var target = new OneOrMoreThreadsPerResourcePolicy(1))
            using (CancellationTokenSource cts1 = new CancellationTokenSource(), cts2 = new CancellationTokenSource())
            {
                target.AcquireLock(cts1.Token);

                cts2.CancelAfter(1000);

                Assert.Throws<OperationCanceledException>(() => target.AcquireLock(cts2.Token));
            }
        }
    }
}
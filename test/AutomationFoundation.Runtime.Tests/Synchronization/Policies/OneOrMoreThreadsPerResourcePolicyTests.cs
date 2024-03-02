using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Runtime.Tests(net472)'
Before:
namespace AutomationFoundation.Runtime.Synchronization.Policies
{
    [TestFixture]
    public class OneOrMoreThreadsPerResourcePolicyTests
    {
        private CancellationTokenSource cancellationSource;

        [SetUp]
        public void Setup()
        {
            cancellationSource = new CancellationTokenSource();
        }

        [TearDown]
        public void Finish()
        {
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
        [Timeout(10000)]
        public async Task PreventMultipleThreadsFromAccessingTheResource()
        {
            using var target = new OneOrMoreThreadsPerResourcePolicy(1);
            await target.AcquireLockAsync(CancellationToken.None);

            cancellationSource.CancelAfter(1000);

            Assert.ThrowsAsync<OperationCanceledException>(async () => await target.AcquireLockAsync(cancellationSource.Token));
        }

        [Test]
        [Timeout(2000)]
        public void ThrowsAnExceptionAfterBeingDisposed()
        {
            var target = new OneOrMoreThreadsPerResourcePolicy(1);
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.AcquireLockAsync(CancellationToken.None));
        }
After:
namespace AutomationFoundation.Runtime.Synchronization.Policies;

[TestFixture]
public class OneOrMoreThreadsPerResourcePolicyTests
{
    private CancellationTokenSource cancellationSource;

    [SetUp]
    public void Setup()
    {
        cancellationSource = new CancellationTokenSource();
    }

    [TearDown]
    public void Finish()
    {
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
    [Timeout(10000)]
    public async Task PreventMultipleThreadsFromAccessingTheResource()
    {
        using var target = new OneOrMoreThreadsPerResourcePolicy(1);
        await target.AcquireLockAsync(CancellationToken.None);

        cancellationSource.CancelAfter(1000);

        Assert.ThrowsAsync<OperationCanceledException>(async () => await target.AcquireLockAsync(cancellationSource.Token));
    }

    [Test]
    [Timeout(2000)]
    public void ThrowsAnExceptionAfterBeingDisposed()
    {
        var target = new OneOrMoreThreadsPerResourcePolicy(1);
        target.Dispose();

        Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.AcquireLockAsync(CancellationToken.None));
*/

namespace AutomationFoundation.Runtime.Synchronization.Policies;

[TestFixture]
public class OneOrMoreThreadsPerResourcePolicyTests
{
    private CancellationTokenSource cancellationSource;

    [SetUp]
    public void Setup()
    {
        cancellationSource = new CancellationTokenSource();
    }

    [TearDown]
    public void Finish()
    {
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
    [Timeout(10000)]
    public async Task PreventMultipleThreadsFromAccessingTheResource()
    {
        using var target = new OneOrMoreThreadsPerResourcePolicy(1);
        await target.AcquireLockAsync(CancellationToken.None);

        cancellationSource.CancelAfter(1000);

        Assert.ThrowsAsync<OperationCanceledException>(async () => await target.AcquireLockAsync(cancellationSource.Token));
    }

    [Test]
    [Timeout(2000)]
    public void ThrowsAnExceptionAfterBeingDisposed()
    {
        var target = new OneOrMoreThreadsPerResourcePolicy(1);
        target.Dispose();

        Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.AcquireLockAsync(CancellationToken.None));
    }
}
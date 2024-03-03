using System;
using System.Threading;
using AutomationFoundation.Runtime.Threading.Primitives;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Threading.Internal;

[TestFixture]
public class WorkerCacheEntryTests
{
    [Test]
    public void ThrowsAnExceptionWhenTheWorkerIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new WorkerCacheEntry(null, DateTime.Now));
    }

    [Test]
    public void SetsTheObjectAsExpected()
    {
        var worker = Mock.Of<Worker>();
        var timestamp = DateTime.Now;

        var target = new WorkerCacheEntry(worker, timestamp);

        Assert.AreEqual(worker, target.Worker);
        Assert.AreEqual(timestamp, target.LastIssued);
    }

    [Test]
    public void ModifiesTheLastIssuedDateAsExpected()
    {
        var worker = Mock.Of<Worker>();
        var original = DateTime.Now;

        var target = new WorkerCacheEntry(worker, original);

        Thread.Sleep(100);
        target.MarkAsIssued();

        Assert.True(target.LastIssued > original);
    }
}
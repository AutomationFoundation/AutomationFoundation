using System;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Runtime.Tests(net472)'
Before:
namespace AutomationFoundation.Runtime
{
    [TestFixture]
    public class PollingSchedulerTests
    {
        [Test]
        public void CalculateTheNextExecutionDateFromStartedDatetimeOffset()
        {
            var interval = TimeSpan.FromSeconds(1);
            var expected = DateTimeOffset.Parse("1/1/2000 12:00:01 AM");

            var target = new PollingScheduler(interval, true);
            var result = target.CalculateNextExecution(DateTimeOffset.Parse("1/1/2000 12:00:00 AM"), DateTimeOffset.Parse("1/1/2000 01:00:00 AM"));

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculateTheNextExecutionDateFromCompletedDatetimeOffset()
        {
            var interval = TimeSpan.FromSeconds(1);
            var expected = DateTimeOffset.Parse("1/1/2000 01:00:01 AM");

            var target = new PollingScheduler(interval);
            var result = target.CalculateNextExecution(DateTimeOffset.Parse("1/1/2000 12:00:00 AM"), DateTimeOffset.Parse("1/1/2000 01:00:00 AM"));

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ThrowAnExceptionWhenTheIntervalIsZero()
        {
            Assert.Throws<ArgumentException>(() => new PollingScheduler(TimeSpan.Zero));
        }

        [Test]
        public void ThrowAnExceptionWhenTheIntervalIsLessThanZero()
        {
            Assert.Throws<ArgumentException>(() => new PollingScheduler(TimeSpan.MinValue));
        }
After:
namespace AutomationFoundation.Runtime;

[TestFixture]
public class PollingSchedulerTests
{
    [Test]
    public void CalculateTheNextExecutionDateFromStartedDatetimeOffset()
    {
        var interval = TimeSpan.FromSeconds(1);
        var expected = DateTimeOffset.Parse("1/1/2000 12:00:01 AM");

        var target = new PollingScheduler(interval, true);
        var result = target.CalculateNextExecution(DateTimeOffset.Parse("1/1/2000 12:00:00 AM"), DateTimeOffset.Parse("1/1/2000 01:00:00 AM"));

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculateTheNextExecutionDateFromCompletedDatetimeOffset()
    {
        var interval = TimeSpan.FromSeconds(1);
        var expected = DateTimeOffset.Parse("1/1/2000 01:00:01 AM");

        var target = new PollingScheduler(interval);
        var result = target.CalculateNextExecution(DateTimeOffset.Parse("1/1/2000 12:00:00 AM"), DateTimeOffset.Parse("1/1/2000 01:00:00 AM"));

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ThrowAnExceptionWhenTheIntervalIsZero()
    {
        Assert.Throws<ArgumentException>(() => new PollingScheduler(TimeSpan.Zero));
    }

    [Test]
    public void ThrowAnExceptionWhenTheIntervalIsLessThanZero()
    {
        Assert.Throws<ArgumentException>(() => new PollingScheduler(TimeSpan.MinValue));
*/

namespace AutomationFoundation.Runtime;

[TestFixture]
public class PollingSchedulerTests
{
    [Test]
    public void CalculateTheNextExecutionDateFromStartedDatetimeOffset()
    {
        var interval = TimeSpan.FromSeconds(1);
        var expected = DateTimeOffset.Parse("1/1/2000 12:00:01 AM");

        var target = new PollingScheduler(interval, true);
        var result = target.CalculateNextExecution(DateTimeOffset.Parse("1/1/2000 12:00:00 AM"), DateTimeOffset.Parse("1/1/2000 01:00:00 AM"));

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculateTheNextExecutionDateFromCompletedDatetimeOffset()
    {
        var interval = TimeSpan.FromSeconds(1);
        var expected = DateTimeOffset.Parse("1/1/2000 01:00:01 AM");

        var target = new PollingScheduler(interval);
        var result = target.CalculateNextExecution(DateTimeOffset.Parse("1/1/2000 12:00:00 AM"), DateTimeOffset.Parse("1/1/2000 01:00:00 AM"));

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ThrowAnExceptionWhenTheIntervalIsZero()
    {
        Assert.Throws<ArgumentException>(() => new PollingScheduler(TimeSpan.Zero));
    }

    [Test]
    public void ThrowAnExceptionWhenTheIntervalIsLessThanZero()
    {
        Assert.Throws<ArgumentException>(() => new PollingScheduler(TimeSpan.MinValue));
    }
}
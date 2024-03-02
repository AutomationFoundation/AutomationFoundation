using System;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Hosting.Tests(net472)'
Before:
namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class HostingEnvironmentTests
    {
        [Test]
        public void ThrowsAnExceptionWhenEnvironmentNameIsNull()
        {
            var target = new HostingEnvironment("Environment");
            Assert.Throws<ArgumentNullException>(() => target.IsEnvironment(null));
        }

        [Test]
        public void ReturnFalseWhenTheEnvironmentDoesNotMatch()
        {
            var target = new HostingEnvironment("Match");
            var match = target.IsEnvironment("ShouldNotMatch");

            Assert.False(match);
        }

        [Test]
        public void ReturnTrueWhenTheEnvironmentMatches()
        {
            var target = new HostingEnvironment("Match");
            var match = target.IsEnvironment("Match");

            Assert.True(match);
        }

        [Test]
        public void ReturnTrueWhenTheEnvironmentStartsTheSame()
        {
            var target = new HostingEnvironment("MatchThisVeryLongEnvironment");
            var match = target.IsEnvironment("Match");

            Assert.True(match);
        }
After:
namespace AutomationFoundation.Hosting;

[TestFixture]
public class HostingEnvironmentTests
{
    [Test]
    public void ThrowsAnExceptionWhenEnvironmentNameIsNull()
    {
        var target = new HostingEnvironment("Environment");
        Assert.Throws<ArgumentNullException>(() => target.IsEnvironment(null));
    }

    [Test]
    public void ReturnFalseWhenTheEnvironmentDoesNotMatch()
    {
        var target = new HostingEnvironment("Match");
        var match = target.IsEnvironment("ShouldNotMatch");

        Assert.False(match);
    }

    [Test]
    public void ReturnTrueWhenTheEnvironmentMatches()
    {
        var target = new HostingEnvironment("Match");
        var match = target.IsEnvironment("Match");

        Assert.True(match);
    }

    [Test]
    public void ReturnTrueWhenTheEnvironmentStartsTheSame()
    {
        var target = new HostingEnvironment("MatchThisVeryLongEnvironment");
        var match = target.IsEnvironment("Match");

        Assert.True(match);
*/

namespace AutomationFoundation.Hosting;

[TestFixture]
public class HostingEnvironmentTests
{
    [Test]
    public void ThrowsAnExceptionWhenEnvironmentNameIsNull()
    {
        var target = new HostingEnvironment("Environment");
        Assert.Throws<ArgumentNullException>(() => target.IsEnvironment(null));
    }

    [Test]
    public void ReturnFalseWhenTheEnvironmentDoesNotMatch()
    {
        var target = new HostingEnvironment("Match");
        var match = target.IsEnvironment("ShouldNotMatch");

        Assert.False(match);
    }

    [Test]
    public void ReturnTrueWhenTheEnvironmentMatches()
    {
        var target = new HostingEnvironment("Match");
        var match = target.IsEnvironment("Match");

        Assert.True(match);
    }

    [Test]
    public void ReturnTrueWhenTheEnvironmentStartsTheSame()
    {
        var target = new HostingEnvironment("MatchThisVeryLongEnvironment");
        var match = target.IsEnvironment("Match");

        Assert.True(match);
    }
}
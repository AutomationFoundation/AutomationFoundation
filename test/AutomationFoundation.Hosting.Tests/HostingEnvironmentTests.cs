using System;
using AutomationFoundation.Hosting.Stubs;
using NUnit.Framework;

namespace AutomationFoundation.Hosting
{
    [TestFixture]
    public class HostingEnvironmentTests
    {
        [Test]
        public void ThrowsAnExceptionWhenEnvironmentNameIsNull()
        {
            var target = new StubHostingEnvironment("Environment");
            Assert.Throws<ArgumentNullException>(() => target.IsEnvironment(null));
        }

        [Test]
        public void ReturnFalseWhenTheEnvironmentDoesNotMatch()
        {
            var target = new StubHostingEnvironment("Match");
            var match = target.IsEnvironment("ShouldNotMatch");

            Assert.False(match);
        }

        [Test]
        public void ReturnTrueWhenTheEnvironmentMatches()
        {
            var target = new StubHostingEnvironment("Match");
            var match = target.IsEnvironment("Match");

            Assert.True(match);
        }

        [Test]
        public void ReturnTrueWhenTheEnvironmentStartsTheSame()
        {
            var target = new StubHostingEnvironment("MatchThisVeryLongEnvironment");
            var match = target.IsEnvironment("Match");

            Assert.True(match);
        }
    }
}
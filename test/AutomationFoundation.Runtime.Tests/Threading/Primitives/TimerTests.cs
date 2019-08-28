using System;
using NUnit.Framework;
using ManualResetEventSlim = System.Threading.ManualResetEventSlim;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        [Timeout(10000)]
        public void RunsTheCallbackAsExpected()
        {
            var called = false;

            using (var waitEvent = new ManualResetEventSlim(false))
            using (var target = new Timer())
            {
                target.Start(TimeSpan.FromSeconds(1), () =>
                {
                    called = true;
                    waitEvent.Set();
                }, error => { });

                waitEvent.Wait();
            }

            Assert.True(called);
        }

        [Test]
        [Timeout(10000)]
        public void RunsTheErrorCallbackAsExpected()
        {
            var called = false;

            using (var waitEvent = new ManualResetEventSlim(false))
            using (var target = new Timer())
            {
                target.Start(TimeSpan.FromSeconds(1), () => throw new InvalidOperationException(), error =>
                {
                    Assert.IsInstanceOf<InvalidOperationException>(error);

                    called = true;
                    waitEvent.Set();
                });

                waitEvent.Wait();
            }

            Assert.True(called);
        }
    }
}
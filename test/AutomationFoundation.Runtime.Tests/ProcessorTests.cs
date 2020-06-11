using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.TestObjects;
using NUnit.Framework;

namespace AutomationFoundation.Runtime
{
    [TestFixture]
    public class ProcessorTests
    {
        private StubProcessor target;
        private string name;

        [SetUp]
        public void Init()
        {
            name = Guid.NewGuid().ToString();
            target = new StubProcessor(name);
        }

        [TearDown]
        public void Cleanup()
        {
            try
            {
                target.Dispose();
            }
            catch (Exception)
            {
                // Just in case a test put the target intentionally into a bad state.
            }
        }

        [Test]
        public void ThrowsAnExceptionWhenStartedAfterDisposed()
        {
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.StartAsync(CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenStoppedAfterDisposed()
        {
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.StopAsync(CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenStartingWhileInTheErrorState()
        {
            target.SetState(ProcessorState.Error);

            Assert.ThrowsAsync<RuntimeException>(async () => await target.StartAsync(CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenStoppingWhileInTheErrorState()
        {
            target.SetState(ProcessorState.Error);

            Assert.ThrowsAsync<RuntimeException>(async () => await target.StopAsync(CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (new StubProcessor(null))
                {
                    // This code block intentionally left blank.
                }
            }, "name");
        }

        [Test]
        public void ThrowsAnExceptionWhenNameIsWhitespace()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (new StubProcessor("   "))
                {
                    // This code block intentionally left blank.
                }
            }, "name");
        }

        [Test]
        public void ReturnsTheCreatedStateUponNew()
        {
            Assert.AreEqual(ProcessorState.Created, target.State);
        }

        [Test]
        public async Task StoppingTwiceThrowsAnException()
        {
            await target.StartAsync(CancellationToken.None);
            await target.StopAsync(CancellationToken.None);

            Assert.ThrowsAsync<RuntimeException>(async () => await target.StopAsync(CancellationToken.None));
        }

        [Test]
        public void SetsTheNamePropertyDuringInitialization()
        {
            Assert.AreEqual(name, target.Name);
        }

        [Test]
        public void ChangeToAnErrorStateWhenExceptionThrownDuringStart()
        {
            target.SetupCallbacks(() => throw new Exception("This is a test exception"));

            Assert.ThrowsAsync<Exception>(async () => await target.StartAsync(CancellationToken.None));
            Assert.AreEqual(ProcessorState.Error, target.State);
        }

        [Test]
        public async Task ChangeToAnErrorStateWhenExceptionThrownDuringStop()
        {
            target.SetupCallbacks(onStopCallback: () => throw new Exception("This is a test exception"));
            await target.StartAsync(CancellationToken.None);

            Assert.ThrowsAsync<Exception>(async () => await target.StopAsync(CancellationToken.None));
            Assert.AreEqual(ProcessorState.Error, target.State);
        }

        [Test]
        public async Task ChangeTheProcessorStatesDuringStart()
        {
            var tested = false;

            target.SetupCallbacks(() =>
            {
                Assert.AreEqual(ProcessorState.Starting, target.State);
                tested = true;
            });

            await target.StartAsync(CancellationToken.None);

            Assert.True(tested);
            Assert.AreEqual(ProcessorState.Started, target.State);
        }

        [Test]
        public async Task ChangeTheProcessorStatesDuringStop()
        {
            var tested = false;

            target.SetupCallbacks(onStopCallback: () =>
            {
                Assert.AreEqual(ProcessorState.Stopping, target.State);
                tested = true;
            });

            await target.StartAsync(CancellationToken.None);
            await target.StopAsync(CancellationToken.None);

            Assert.True(tested);
            Assert.AreEqual(ProcessorState.Stopped, target.State);
        }

        [Test]
        public async Task ThrowAnExceptionWhenTheProcessorIsAlreadyStarted()
        {
            await target.StartAsync(CancellationToken.None);

            Assert.AreEqual(ProcessorState.Started, target.State);
            Assert.Throws<RuntimeException>(() => target.ExecuteGuardMustNotAlreadyBeStarted());
        }

        [Test]
        public async Task ThrowAnExceptionWhenTheProcessorIsAlreadyStopped()
        {
            await target.StartAsync(CancellationToken.None);
            Assert.AreEqual(ProcessorState.Started, target.State);

            await target.StopAsync(CancellationToken.None);
            Assert.AreEqual(ProcessorState.Stopped, target.State);

            Assert.Throws<RuntimeException>(() => target.ExecuteGuardMustNotAlreadyBeStopped());
        }

        [Test]
        public void DisposeOfAProcessorThatIsNeverStarted()
        {
            var called = false;

            target.SetupCallbacks(onDispose: () => called = true);

            target.Dispose();

            Assert.IsTrue(called);
        }
    }
}

using System.Threading.Tasks;
using AutomationFoundation.Hosting;
using AutomationFoundation.Interop;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using static AutomationFoundation.Interop.ConsoleApi;

namespace AutomationFoundation.NETFX.App
{
    [TestFixture]
    public class TaskKillRuntimeHostRunAsyncStrategyTests
    {
        private TestableTaskKillRuntimeHostRunAsyncStrategy target;
        private Mock<IKernel32> kernel32;
        private Mock<ILogger<TaskKillRuntimeHostRunAsyncStrategy>> logger;
        private TaskKillRuntimeHostRunAsyncOptions options;

        private Mock<IRuntimeHost> host;

        [SetUp]
        public void Init()
        {
            host = new Mock<IRuntimeHost>();

            kernel32 = new Mock<IKernel32>();
            logger = new Mock<ILogger<TaskKillRuntimeHostRunAsyncStrategy>>();
            options = new TaskKillRuntimeHostRunAsyncOptions();

            target = new TestableTaskKillRuntimeHostRunAsyncStrategy(kernel32.Object, logger.Object, options);
        }

        [TearDown]
        public void Cleanup()
        {
            target?.Dispose();
        }

        [Test]
        public async Task TerminatesTheApplicationOnCtrlCloseEvent()
        {
            var task = target.RunAsync(host.Object);

            var result = target.SimulateCtrlMessageReceived(CTRL_CLOSE_EVENT);
            await task;

            Assert.True(result);
            Assert.True(target.WaitedUntilTerminated);
        }

        [Test]
        public async Task TerminatesTheApplicationOnCtrlLogOffEvent()
        {
            var task = target.RunAsync(host.Object);

            var result = target.SimulateCtrlMessageReceived(CTRL_LOGOFF_EVENT);
            await task;

            Assert.True(result);
            Assert.True(target.WaitedUntilTerminated);
        }

        [Test]
        public async Task DoesNotTerminateTheApplicationOnUnknownEvent()
        {
            var task = target.RunAsync(host.Object);

            var result = target.SimulateCtrlMessageReceived(-1);
            await task;

            Assert.True(result);
            Assert.True(target.WaitedUntilSignaled);
        }
    }
}
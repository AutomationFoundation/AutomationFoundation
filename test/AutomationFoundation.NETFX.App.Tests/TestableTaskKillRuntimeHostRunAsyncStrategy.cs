using System.Threading.Tasks;
using AutomationFoundation.Interop;
using Microsoft.Extensions.Logging;

namespace AutomationFoundation.NETFX.App
{
    internal class TestableTaskKillRuntimeHostRunAsyncStrategy : TaskKillRuntimeHostRunAsyncStrategy
    {
        public bool WaitedUntilTerminated { get; private set; }
        public bool WaitedUntilSignaled { get; private set; }

        internal TestableTaskKillRuntimeHostRunAsyncStrategy(IKernel32 kernel32, ILogger<TaskKillRuntimeHostRunAsyncStrategy> logger, TaskKillRuntimeHostRunAsyncOptions options) :
            base(kernel32, logger, options)
        {
        }

        public bool SimulateCtrlMessageReceived(int dwCtrlType)
        {
            return OnCtrlMessageReceived(dwCtrlType);
        }

        protected override void WaitUntilTerminated()
        {
            WaitedUntilTerminated = true;
        }

        protected override Task DelayUntilSignaledAsync()
        {
            WaitedUntilSignaled = true;
            return Task.CompletedTask;
        }
    }
}
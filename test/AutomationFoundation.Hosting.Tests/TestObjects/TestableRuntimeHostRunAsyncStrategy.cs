using System;
using System.Threading.Tasks;

namespace AutomationFoundation.Hosting.TestObjects
{
    public class TestableRuntimeHostRunAsyncStrategy : RuntimeHostRunAsyncStrategy
    {
        public bool CanceledWhileRunning { get; private set; }
        public bool CanceledWhileStopping { get; private set; }

        protected override Task AttachToListenForExitAsync()
        {
            return Task.CompletedTask;
        }

        public void Cancel()
        {
            CancellationSource.Cancel();
        }

        public void CancelAfter(TimeSpan delay)
        {
            CancellationSource.CancelAfter(delay);
        }

        protected override Task OnStoppedAsync()
        {
            return Task.CompletedTask;
        }

        protected override async Task OnCanceledWhileRunningAsync()
        {
            CanceledWhileRunning = true;
            await base.OnCanceledWhileRunningAsync();
        }

        protected override async Task OnCanceledWhileStoppingAsync()
        {
            CanceledWhileStopping = true;
            await base.OnCanceledWhileStoppingAsync();
        }
    }
}
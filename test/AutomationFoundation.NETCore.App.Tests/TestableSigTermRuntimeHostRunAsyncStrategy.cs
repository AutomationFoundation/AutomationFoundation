using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutomationFoundation.NETCore.App
{
    internal class TestableSigTermRuntimeHostRunAsyncStrategy : SigTermRuntimeHostRunAsyncStrategy
    {
        public bool AttachedToProcessExitEvent { get; private set; }
        public bool Stopped { get; private set; }        
        public bool FlaggedForCancellation => CancellationSource.IsCancellationRequested;

        public TestableSigTermRuntimeHostRunAsyncStrategy(ILogger<SigTermRuntimeHostRunAsyncStrategy> logger, IOptions<SigTermRuntimeHostRunAsyncOptions> options) 
            : base(logger, options)
        {
        }

        public async Task SimulateProcessExitedAsync()
        { 
            await OnProcessExitedAsync();
        }

        public async Task SimulateBeingStoppingAsync()
        {
            await OnStoppedAsync();
        }

        public async Task SimulateBeingRanAsync()
        {
            await base.AttachToListenForExitAsync();
        }

        protected override void AttachToProcessExitEvent()
        {
            AttachedToProcessExitEvent = true;
        }

        protected override Task OnStoppedAsync()
        {
            Stopped = true;
            return base.OnStoppedAsync();
        }
    }
}
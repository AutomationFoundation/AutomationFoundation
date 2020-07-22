using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutomationFoundation.TestObjects
{
    internal class TestableCtrlCRuntimeHostRunAsyncStrategy : CtrlCRuntimeHostRunAsyncStrategy
    {
        public bool AttachedToCtrlCKeyPressEvent { get; private set; }
        public bool Stopped { get; private set; }        
        public bool FlaggedForCancellation => CancellationSource.IsCancellationRequested;

        public TestableCtrlCRuntimeHostRunAsyncStrategy(ILogger<CtrlCRuntimeHostRunAsyncStrategy> logger, IOptions<CtrlCRuntimeHostRunAsyncOptions> options) 
            : base(logger, options)
        {
        }

        public async Task<bool> SimulateCtrlCKeyPressedAsync()
        {
            return await OnCancelKeyPressed();
        }

        public async Task SimulateBeingStoppingAsync()
        {
            await OnStoppedAsync();
        }

        public async Task SimulateBeingRanAsync()
        {
            await base.AttachToListenForExitAsync();
        }

        protected override void AttachToCtrlCKeyPressEvent()
        {
            AttachedToCtrlCKeyPressEvent = true;
        }

        protected override Task OnStoppedAsync()
        {
            Stopped = true;
            return base.OnStoppedAsync();
        }
    }
}
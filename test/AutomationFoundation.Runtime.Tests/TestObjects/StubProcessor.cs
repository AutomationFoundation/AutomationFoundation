using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.TestObjects
{
    public class StubProcessor : Processor
    {
        private Action onStartCallback;
        private Action onStopCallback;
        private Action onDisposeCallback;

        public StubProcessor(string name)
            : base(name)
        {            
        }

        protected override Task OnStartAsync(CancellationToken cancellationToken)
        {
            onStartCallback?.Invoke();
            return Task.CompletedTask;
        }

        protected override Task OnStopAsync(CancellationToken cancellationToken)
        {
            onStopCallback?.Invoke();
            return Task.CompletedTask;
        }

        protected override void Dispose(bool disposing)
        {
            onDisposeCallback?.Invoke();
            base.Dispose(disposing);
        }

        public void SetupCallbacks(Action startCallback = null, Action stopCallback = null, Action disposeCallback = null)
        {
            onStartCallback = startCallback;
            onStopCallback = stopCallback;
            onDisposeCallback = disposeCallback;
        }

        public void SetState(ProcessorState value)
        {
            State = value;
        }

        public void SimulatedCustomCapability()
        {
            GuardMustNotBeDisposed();
        }
    }
}
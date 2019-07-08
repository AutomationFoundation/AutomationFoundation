using System;

namespace AutomationFoundation.Runtime.Tests.Stubs
{
    public class StubDisposableObject : DisposableObject
    {
        private readonly Action<bool> onDisposeCallback;

        public StubDisposableObject(Action<bool> onDisposeCallback = null)
        {
            this.onDisposeCallback = onDisposeCallback;
        }

        public new bool Disposed => base.Disposed;

        protected override void Dispose(bool disposing)
        {
            onDisposeCallback?.Invoke(disposing);

            base.Dispose(disposing);
        }
    }
}
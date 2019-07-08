using System;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Represents a disposable object. This class must be inherited.
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the object has been disposed.
        /// </summary>
        protected bool Disposed { get; private set; }

        /// <summary>
        /// Finalizes the object instance.
        /// </summary>
        ~DisposableObject()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes of the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of the object.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources, false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disposed = true;
            }
        }

        /// <summary>
        /// Ensures the object has not been disposed.
        /// </summary>
        protected void GuardMustNotBeDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException("The object has been disposed.");
            }
        }
    }
}
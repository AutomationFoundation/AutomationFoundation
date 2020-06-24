using System.Runtime.ConstrainedExecution;

namespace AutomationFoundation.Runtime.Synchronization
{
    /// <summary>
    /// Provides a lock on a synchronization resource.
    /// </summary>
    public abstract class SynchronizationLock : CriticalFinalizerObject, ISynchronizationLock
    {
        /// <summary>
        /// Gets the object used for synchronization.
        /// </summary>
        protected object SyncRoot { get; } = new object();

        /// <summary>
        /// Releases the lock on the resource.
        /// </summary>
        public void Release()
        {
            lock (SyncRoot)
            {
                ReleaseLock();
            }
        }

        /// <summary>
        /// Releases the lock on the resource.
        /// </summary>
        protected abstract void ReleaseLock();
    }
}
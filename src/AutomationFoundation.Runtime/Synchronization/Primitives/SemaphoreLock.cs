using System;
using System.Threading;

namespace AutomationFoundation.Runtime.Synchronization.Primitives
{
    /// <summary>
    /// Provides a resource lock using a semaphore.
    /// </summary>
    public class SemaphoreLock : SynchronizationLock
    {
        private readonly SemaphoreSlim semaphore;

        /// <summary>
        /// Initializes a new instance of the <see cref="SemaphoreLock"/> class.
        /// </summary>
        /// <param name="semaphore">The semaphore which owns the lock.</param>
        public SemaphoreLock(SemaphoreSlim semaphore)
        {
            this.semaphore = semaphore ?? throw new ArgumentNullException(nameof(semaphore));
        }

        /// <inheritdoc />
        protected override void ReleaseLock()
        {
            semaphore.Release();
        }
    }
}
using System;
using System.Threading;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using AutomationFoundation.Runtime.Synchronization.Primitives;

namespace AutomationFoundation.Runtime.Synchronization.Policies
{
    /// <summary>
    /// Provides a synchronization policy allowing one or more threads to access a single resource.
    /// </summary>
    public class OneOrMoreThreadsPerResourcePolicy : DisposableObject, ISynchronizationPolicy
    {
        private readonly SemaphoreSlim semaphore;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneOrMoreThreadsPerResourcePolicy"/> class.
        /// </summary>
        /// <param name="maximum">The maximum number of threads allowed to access the resource.</param>
        public OneOrMoreThreadsPerResourcePolicy(int maximum)
        {
            if (maximum <= 0)
            {
                throw new ArgumentException($"{nameof(maximum)} must be greater than zero.", nameof(maximum));
            }

            semaphore = new SemaphoreSlim(maximum, maximum);
        }

        /// <inheritdoc />
        public ISynchronizationLock AcquireLock(CancellationToken cancellationToken)
        {
            semaphore.Wait(cancellationToken);

            return new SemaphoreLock(semaphore);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                semaphore.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
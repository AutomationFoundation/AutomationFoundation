using System;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Represents an entry within a worker pool cache.
    /// </summary>
    internal class WorkerCacheEntry
    {
        private readonly object syncRoot = new object();

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets the worker instance.
        /// </summary>
        public Worker Worker { get; }

        /// <summary>
        /// Gets the timestamp when the entry was created.
        /// </summary>
        public DateTime Created { get; } = DateTime.Now;

        /// <summary>
        /// Gets the timestamp when the worker was last issued.
        /// </summary>
        public DateTime LastIssued { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the entry is enabled.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerCacheEntry"/> class.
        /// </summary>
        /// <param name="worker">The worker instance.</param>
        /// <param name="lastIssued">The timestamp when the worker was last issued.</param>
        public WorkerCacheEntry(Worker worker, DateTime lastIssued)
        {
            Worker = worker ?? throw new ArgumentNullException(nameof(worker));
            LastIssued = lastIssued;
        }

        /// <summary>
        /// Marks the entry as being issued.
        /// </summary>
        public void MarkAsIssued()
        {
            lock (syncRoot)
            {
                LastIssued = DateTime.Now;
            }
        }

        /// <summary>
        /// Determines whether the entry has expired.
        /// </summary>
        /// <param name="frequencyDuration">The duration from when the worker must be last issued.</param>
        /// <param name="maximumDuration">The duration from when the worker was created.</param>
        /// <returns>A value indicating whether the item has expired.</returns>
        public bool HasExpired(TimeSpan frequencyDuration, TimeSpan maximumDuration)
        {
            var now = DateTime.Now;

            lock (syncRoot)
            {
                return (now - LastIssued >= frequencyDuration || now - Created >= maximumDuration)
                       && !Worker.IsRunning;
            }
        }
    }
}
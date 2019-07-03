using System;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Provides a mechanism which monitors a worker cache instance.
    /// </summary>
    internal class WorkerCacheMonitor : DisposableObject, IWorkerCacheMonitor
    {
        private readonly ITimer timer;
        private readonly IWorkerCacheEntries cache;
        private readonly WorkerPoolOptions options;

        public WorkerCacheMonitor(ITimer timer, IWorkerCacheEntries cache, WorkerPoolOptions options)
        {
            this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc />
        public void Start()
        {
            GuardMustNotBeDisposed();

            timer.Start(options.PollingInterval, OnTimerElapsed, OnErrorOccurred);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer.Dispose();
            }

            base.Dispose(disposing);
        }

        private void OnTimerElapsed()
        {
            try
            {
                ProcessCacheEntries();
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex);
            }
        }

        private void ProcessCacheEntries()
        {
            var entries = cache.GetEntries();
            if (entries == null)
            {
                return;
            }

            try
            {
                foreach (var entry in entries)
                {
                    if (entry.HasExpired(options.Duration, options.MaximumDuration))
                    {
                        lock (entry)
                        {
                            entry.IsEnabled = false;
                        }
                    }
                }
            }
            finally
            {
                cache.CleanUp();
            }
        }

        private void OnErrorOccurred(Exception error)
        {
        }
    }
}
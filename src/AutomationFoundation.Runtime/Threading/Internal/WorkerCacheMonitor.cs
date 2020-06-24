using System;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Provides a mechanism which monitors a worker cache instance.
    /// </summary>
    internal class WorkerCacheMonitor : IWorkerCacheMonitor, IDisposable
    {
        private readonly ITimer timer;
        private readonly IWorkerCacheEntries cache;
        private readonly WorkerPoolOptions options;

        private bool disposed;

        public WorkerCacheMonitor(ITimer timer, IWorkerCacheEntries cache, WorkerPoolOptions options)
        {
            this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        ~WorkerCacheMonitor()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Start()
        {
            GuardMustNotBeDisposed();

            timer.Start(options.PollingInterval, ProcessCacheEntries, OnErrorOccurred);
        }

        /// <summary>
        /// Guards against the monitor having been disposed.
        /// </summary>
        protected void GuardMustNotBeDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(WorkerCacheMonitor));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer.Dispose();
            }

            disposed = true;
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

        private void OnErrorOccurred(Exception ex)
        {
            // Swallow the exception to ensure the error does not cause a fatal exception.
        }
    }
}
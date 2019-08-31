using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Provides a cache of workers.
    /// </summary>
    internal class WorkerCache : IWorkerCache, IWorkerCacheEntries, IDisposable
    {
        private readonly ConcurrentBag<WorkerCacheEntry> pendingCleanup = new ConcurrentBag<WorkerCacheEntry>();
        private readonly ConcurrentDictionary<IWorker, WorkerCacheEntry> busy = new ConcurrentDictionary<IWorker, WorkerCacheEntry>();
        private readonly ManualResetEventSlim waitHandle = new ManualResetEventSlim(true);

        private ConcurrentQueue<WorkerCacheEntry> available = new ConcurrentQueue<WorkerCacheEntry>();
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerCache"/> class.
        /// </summary>
        public WorkerCache()
        {
        }

        ~WorkerCache()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public IRuntimeWorker Get()
        {
            GuardMustNotBeDisposed();
            WaitIfLocked();

            Worker worker = null;

            try
            {
                if (!TryGetAvailableWorker(out worker))
                {
                    worker = CreateWorker();
                    if (worker == null)
                    {
                        throw new InvalidOperationException("The worker was not created.");
                    }
                }
            }
            catch (Exception)
            {
                if (worker != null)
                {
                    Release(worker);
                }

                throw;
            }

            return worker;
        }

        /// <summary>
        /// Attempts to get an available worker.
        /// </summary>
        /// <param name="worker">Upon return, contains the worker which was assigned (if available).</param>
        /// <returns>true if the worker was assigned, otherwise false.</returns>
        protected virtual bool TryGetAvailableWorker(out Worker worker)
        {
            if (available.TryDequeue(out var entry))
            {
                if (entry.IsEnabled)
                {
                    busy.TryAdd(entry.Worker, entry);
                }
                else
                {
                    pendingCleanup.Add(entry);
                    entry = null;
                }
            }

            if (entry != null)
            {
                entry.MarkAsIssued();

                worker = entry.Worker;
                return true;
            }

            worker = null;
            return false;
        }

        /// <summary>
        /// Creates a new entry.
        /// </summary>
        /// <returns>The new entry instance, which includes a worker.</returns>
        protected virtual Worker CreateWorker()
        {
            Worker worker = null;

            try
            {
                worker = new Worker();

                busy[worker] = new WorkerCacheEntry(worker, DateTime.Now);

                return worker;
            }
            catch (Exception)
            {
                worker?.Dispose();
                throw;
            }
        }

        /// <inheritdoc />
        public void Release(IRuntimeWorker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }

            GuardMustNotBeDisposed();
            WaitIfLocked();

            if (busy.TryRemove(worker, out var entry))
            {
                entry.Worker.Reset();

                available.Enqueue(entry);
            }
        }

        /// <inheritdoc />
        public IEnumerable<WorkerCacheEntry> GetEntries()
        {
            GuardMustNotBeDisposed();
            WaitIfLocked();

            try
            {
                Lock();

                var result = new List<WorkerCacheEntry>();

                result.AddRange(available);
                result.AddRange(busy.Values);

                return result.AsReadOnly();
            }
            finally
            {
                Unlock();
            }
        }

        /// <inheritdoc />
        public void CleanUp()
        {
            GuardMustNotBeDisposed();

            try
            {
                Lock();
                EnsureDisabledEntriesArePendingCleanUp();

                while (pendingCleanup.TryTake(out var entry))
                {
                    entry.Worker.Dispose();
                }
            }
            finally
            {
                Unlock();
            }
        }

        private void EnsureDisabledEntriesArePendingCleanUp()
        {
            var temp = new ConcurrentQueue<WorkerCacheEntry>();

            try
            {
                while (available.TryDequeue(out var entry))
                {
                    if (entry.IsEnabled)
                    {
                        temp.Enqueue(entry);
                    }
                    else
                    {
                        pendingCleanup.Add(entry);
                    }
                }
            }
            finally
            {
                available = temp;
            }
        }

        private void Lock()
        {
            waitHandle.Reset();
        }

        private void Unlock()
        {
            waitHandle.Set();
        }

        private void WaitIfLocked()
        {
            waitHandle.Wait();
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
                waitHandle.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Guards against the worker cache having been disposed.
        /// </summary>
        protected void GuardMustNotBeDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(WorkerCache));
            }
        }
    }
}
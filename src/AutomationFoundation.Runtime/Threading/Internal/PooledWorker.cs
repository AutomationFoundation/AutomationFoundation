using System;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Represents a pooled worker.
    /// </summary>
    internal sealed class PooledWorker : IWorker
    {
        private readonly IWorkerCache cache;
        private readonly IRuntimeWorker worker;

        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledWorker"/> class.
        /// </summary>
        /// <param name="cache">The cache which sourced the worker.</param>
        /// <param name="worker"></param>
        public PooledWorker(IWorkerCache cache, IRuntimeWorker worker)
        {
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        ~PooledWorker()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Run()
        {
            GuardMustNotBeDisposed();

            worker.Run();
        }

        /// <inheritdoc />
        public Task RunAsync()
        {
            GuardMustNotBeDisposed();

            return worker.RunAsync();
        }

        /// <inheritdoc />
        public void WaitForCompletion()
        {
            GuardMustNotBeDisposed();

            worker.WaitForCompletion();
        }

        /// <inheritdoc />
        public Task WaitForCompletionAsync()
        {
            GuardMustNotBeDisposed();

            return worker.WaitForCompletionAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Release();
            }

            disposed = true;
        }

        private void Release()
        {
            cache.Release(worker);
        }

        /// <summary>
        /// Guards against the worker having been disposed.
        /// </summary>
        private void GuardMustNotBeDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(WorkerCache));
            }
        }
    }
}
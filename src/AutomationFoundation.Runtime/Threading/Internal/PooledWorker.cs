using System;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Represents a pooled worker.
    /// </summary>
    internal sealed class PooledWorker : DisposableObject, IWorker
    {
        private readonly IWorkerCache cache;
        private readonly IRuntimeWorker worker;

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

        /// <inheritdoc />
        public void Run()
        {
            worker.Run();
        }

        /// <inheritdoc />
        public Task RunAsync()
        {
            return worker.RunAsync();
        }

        /// <inheritdoc />
        public void WaitForCompletion()
        {
            worker.WaitForCompletion();
        }

        /// <inheritdoc />
        public Task WaitForCompletionAsync()
        {
            return worker.WaitForCompletionAsync();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Release();
            }

            base.Dispose(disposing);
        }

        private void Release()
        {
            cache.Release(worker);
        }
    }
}
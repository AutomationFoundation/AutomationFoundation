using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Identifies a cache of workers.
    /// </summary>
    internal interface IWorkerCache
    {
        /// <summary>
        /// Gets an available worker.
        /// </summary>
        /// <returns>The worker instance.</returns>
        IRuntimeWorker Get();

        /// <summary>
        /// Releases the worker.
        /// </summary>
        /// <param name="worker">The worker to release.</param>
        void Release(IRuntimeWorker worker);
    }
}
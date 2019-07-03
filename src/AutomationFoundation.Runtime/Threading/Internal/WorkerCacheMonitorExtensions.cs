using System;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Contains extension methods for the <see cref="IWorkerCacheMonitor"/> interface.
    /// </summary>
    internal static class WorkerCacheMonitorExtensions
    {
        /// <summary>
        /// Disposes of the object.
        /// </summary>
        /// <param name="cacheMonitor">The cache monitor instance to dispose.</param>
        public static void DisposeIfNecessary(this IWorkerCacheMonitor cacheMonitor)
        {
            if (cacheMonitor == null)
            {
                throw new ArgumentNullException(nameof(cacheMonitor));
            }

            (cacheMonitor as IDisposable)?.Dispose();
        }
    }
}
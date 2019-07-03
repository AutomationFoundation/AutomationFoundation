using System;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Contains extension methods for the <see cref="IWorkerCache"/> interface.
    /// </summary>
    internal static class WorkerCacheExtensions
    {
        /// <summary>
        /// Disposes of the object.
        /// </summary>
        /// <param name="cache">The cache instance to dispose.</param>
        public static void DisposeIfNecessary(this IWorkerCache cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            (cache as IDisposable)?.Dispose();
        }
    }
}
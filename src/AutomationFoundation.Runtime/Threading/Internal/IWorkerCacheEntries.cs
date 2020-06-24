using System.Collections.Generic;

namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Identifies an mechanism which interacts with the entries within a cache.
    /// </summary>
    internal interface IWorkerCacheEntries
    {
        /// <summary>
        /// Retrieves the entries within the cache.
        /// </summary>
        /// <returns></returns>
        IEnumerable<WorkerCacheEntry> GetEntries();

        /// <summary>
        /// Cleans the cache.
        /// </summary>
        void CleanUp();
    }
}
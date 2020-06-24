namespace AutomationFoundation.Runtime.Threading.Internal
{
    /// <summary>
    /// Identifies a mechanism which monitors a worker cache.
    /// </summary>
    internal interface IWorkerCacheMonitor
    {
        /// <summary>
        /// Starts monitoring the cache.
        /// </summary>
        void Start();
    }
}
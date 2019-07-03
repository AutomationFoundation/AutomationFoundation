namespace AutomationFoundation.Runtime.Abstractions.Synchronization
{
    /// <summary>
    /// Identifies an object used for synchronizing access to protected resources.
    /// </summary>
    public interface ISynchronizationLock
    {
        /// <summary>
        /// Releases the lock on the resource.
        /// </summary>
        void Release();
    }
}
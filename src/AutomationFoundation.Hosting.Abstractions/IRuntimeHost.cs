namespace AutomationFoundation.Hosting.Abstractions
{
    /// <summary>
    /// Identifies a host for a runtime.
    /// </summary>
    public interface IRuntimeHost
    {
        /// <summary>
        /// Starts the host.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the host.
        /// </summary>
        void Stop();
    }
}
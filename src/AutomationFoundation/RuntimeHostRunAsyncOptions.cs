using System.Threading;

namespace AutomationFoundation
{
    /// <summary>
    /// Defines the options for an <see cref="RuntimeHostRunAsyncStrategy"/>.
    /// </summary>
    public class RuntimeHostRunAsyncOptions
    {
        /// <summary>
        /// Gets or sets the timeout (in milliseconds) upon which the startup will be aborted.
        /// </summary>
        public int StartupTimeoutMs { get; set; } = Timeout.Infinite;

        /// <summary>
        /// Gets or sets the timeout (in milliseconds) upon which the shutdown will no longer be graceful, and will be forced.
        /// </summary>
        public int ShutdownTimeoutMs { get; set; } = Timeout.Infinite;
    }
}
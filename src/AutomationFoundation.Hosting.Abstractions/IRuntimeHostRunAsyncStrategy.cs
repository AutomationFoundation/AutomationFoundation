using System.Threading.Tasks;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Identifies a strategy for running the <see cref="IRuntimeHost"/>.
    /// </summary>
    public interface IRuntimeHostRunAsyncStrategy
    {
        /// <summary>
        /// Runs the host.
        /// </summary>
        /// <param name="host">The host to run.</param>
        /// <param name="startupTimeoutMs">The timeout (in milliseconds) upon which startup will be aborted.</param>
        /// <param name="shutdownTimeoutMs">The timeout (in milliseconds) upon which shutdown will no longer be graceful, and shall be forced.</param>
        /// <returns>The task to await.</returns>
        Task RunAsync(IRuntimeHost host, int startupTimeoutMs, int shutdownTimeoutMs);
    }
}
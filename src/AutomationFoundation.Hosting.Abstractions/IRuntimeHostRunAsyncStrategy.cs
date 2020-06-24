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
        /// <returns>The task to await.</returns>
        Task RunAsync(IRuntimeHost host);
    }
}
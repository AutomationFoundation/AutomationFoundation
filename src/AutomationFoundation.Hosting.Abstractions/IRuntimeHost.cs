using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Identifies a host for a runtime.
    /// </summary>
    public interface IRuntimeHost : IDisposable
    {
        /// <summary>
        /// Gets the application services.
        /// </summary>
        IServiceProvider ApplicationServices { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        IHostingEnvironment Environment { get; }

        /// <summary>
        /// Starts the host.
        /// </summary>
        /// <param name="cancellationToken">Indicates when the start process has been aborted.</param>
        Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Stops the host.
        /// </summary>
        /// <param name="cancellationToken">Indicates when the stop process should no longer be graceful.</param>
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
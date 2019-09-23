using System;

namespace AutomationFoundation.Hosting.Abstractions
{
    /// <summary>
    /// Identifies a host for a runtime.
    /// </summary>
    public interface IRuntimeHost
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
        void Start();

        /// <summary>
        /// Stops the host.
        /// </summary>
        void Stop();
    }
}
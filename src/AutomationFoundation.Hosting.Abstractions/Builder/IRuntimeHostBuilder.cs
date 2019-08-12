using System;
using AutomationFoundation.Runtime.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Hosting.Abstractions.Builder
{
    /// <summary>
    /// Identifies a builder for a runtime host.
    /// </summary>
    public interface IRuntimeHostBuilder : IBuilder<IRuntimeHost>
    {
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="callback">The callback which will configure the services.</param>
        /// <returns>The current runtime host builder instance.</returns>
        IRuntimeHostBuilder ConfigureServices(Action<IServiceCollection> callback);

        /// <summary>
        /// Identifies the startup type which should be used.
        /// </summary>
        /// <typeparam name="TStartup">The type of startup to use when starting the host.</typeparam>
        /// <returns>The current runtime host builder instance.</returns>
        IRuntimeHostBuilder UseStartup<TStartup>() where TStartup : IStartup;

        /// <summary>
        /// Identifies the startup instance which should be used.
        /// </summary>
        /// <param name="startup">The startup instance which should be used during runtime startup.</param>
        /// <returns>The current runtime host builder instance.</returns>
        IRuntimeHostBuilder UseStartup(IStartup startup);
    }
}
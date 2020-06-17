using System;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Hosting.Abstractions.Builders
{
    /// <summary>
    /// Identifies a builder for a runtime host.
    /// </summary>
    public interface IRuntimeHostBuilder
    {
        /// <summary>
        /// Configures the environment.
        /// </summary>
        /// <param name="callback">The callback which will configure the environment.</param>
        /// <returns>The current runtime host builder instance.</returns>
        IRuntimeHostBuilder ConfigureHostingEnvironment(Action<IHostingEnvironmentBuilder> callback);

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="callback">The callback which will configure the services.</param>
        /// <returns>The current runtime host builder instance.</returns>
        IRuntimeHostBuilder ConfigureServices(Action<IServiceCollection> callback);

        /// <summary>
        /// Configures the strategy used for running the host until triggered to stop.
        /// </summary>
        /// <typeparam name="TStrategy">The type of strategy to use.</typeparam>
        /// <returns>The current runtime host builder instance.</returns>
        IRuntimeHostBuilder UseRunStrategy<TStrategy>() where TStrategy : IRuntimeHostRunAsyncStrategy;

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

        /// <summary>
        /// Builds the host.
        /// </summary>
        /// <returns>The new host which was built.</returns>
        IRuntimeHost Build();
    }
}
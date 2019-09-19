using System;
using AutomationFoundation.Hosting.Abstractions;
using AutomationFoundation.Hosting.Abstractions.Builders;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Provides a host for the runtime.
    /// </summary>
    public class RuntimeHost : IRuntimeHost
    {
        private readonly IRuntime runtime;

        /// <summary>
        /// Gets the application services available.
        /// </summary>
        public IServiceProvider ApplicationServices { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeHost"/> class.
        /// </summary>
        /// <param name="runtime">The runtime to host.</param>
        /// <param name="applicationServices">The application services available.</param>
        public RuntimeHost(IRuntime runtime, IServiceProvider applicationServices)
        {
            this.runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
            ApplicationServices = applicationServices;
        }

        /// <inheritdoc />
        public void Start()
        {
            runtime.Start();
        }

        /// <inheritdoc />
        public void Stop()
        {
            runtime.Stop();
        }

        /// <summary>
        /// Create a runtime host builder.
        /// </summary>
        /// <returns>The runtime host builder.</returns>
        public static TBuilder CreateBuilder<TBuilder>()
            where TBuilder : IRuntimeHostBuilder, new()
        {
            return new TBuilder();
        }
    }
}
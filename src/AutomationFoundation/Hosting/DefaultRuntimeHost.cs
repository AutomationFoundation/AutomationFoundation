using System;
using AutomationFoundation.Hosting.Abstractions;
using AutomationFoundation.Hosting.Abstractions.Builders;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Represents a default runtime host. This class cannot be inherited.
    /// </summary>
    public sealed class DefaultRuntimeHost : RuntimeHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRuntimeHost"/> class.
        /// </summary>
        /// <param name="runtime">The runtime to host.</param>
        /// <param name="environment">The hosting environment.</param>
        /// <param name="applicationServices">The application services available.</param>
        public DefaultRuntimeHost(IRuntime runtime, IHostingEnvironment environment, IServiceProvider applicationServices)
            : base(runtime, environment, applicationServices)
        {
        }

        /// <summary>
        /// Create a runtime host builder.
        /// </summary>
        /// <returns>The runtime host builder.</returns>
        public static IRuntimeHostBuilder CreateBuilder()
        {
            return new DefaultRuntimeHostBuilder();
        }
    }
}
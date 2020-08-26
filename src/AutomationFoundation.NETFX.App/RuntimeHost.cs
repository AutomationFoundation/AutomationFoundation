using System;
using AutomationFoundation.Hosting;
using AutomationFoundation.Runtime;

namespace AutomationFoundation
{
    /// <summary>
    /// Provides a host for the runtime.
    /// </summary>
    public sealed class RuntimeHost : RuntimeHostBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeHost"/> class.
        /// </summary>
        /// <param name="runtime">The runtime to host.</param>
        /// <param name="environment">The hosting environment.</param>
        /// <param name="applicationServices">The application services available.</param>
        public RuntimeHost(IRuntime runtime, IHostingEnvironment environment, IServiceProvider applicationServices) 
            : base(runtime, environment, applicationServices)
        {
        }

        /// <summary>
        /// Creates a default runtime host builder.
        /// </summary>
        /// <returns>The builder instance.</returns>
        public static IRuntimeHostBuilder CreateDefaultBuilder()
        {
            return new DefaultRuntimeHostBuilder();
        }
    }
}
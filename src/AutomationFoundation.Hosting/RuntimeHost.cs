using System;
using System.Threading;
using System.Threading.Tasks;
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

        /// <inheritdoc />
        public IServiceProvider ApplicationServices { get; }

        /// <inheritdoc />
        public IHostingEnvironment Environment { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeHost"/> class.
        /// </summary>
        /// <param name="runtime">The runtime to host.</param>
        /// <param name="environment">The hosting environment.</param>
        /// <param name="applicationServices">The application services available.</param>
        public RuntimeHost(IRuntime runtime, IHostingEnvironment environment, IServiceProvider applicationServices)
        {
            this.runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            ApplicationServices = applicationServices;
        }

        ~RuntimeHost()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                runtime.Dispose();
            }
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await runtime.StartAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await runtime.StopAsync(cancellationToken);
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
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Identifies an object which provides contextual information for an item being processed by the runtime.
    /// </summary>
    public interface IProcessingContext : IDisposable
    {
        /// <summary>
        /// Gets the identifier of the item being processed.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the lifetime scope.
        /// </summary>
        IServiceScope LifetimeScope { get; }

        /// <summary>
        /// Gets or sets the cancellation token to monitor for cancellation requests.
        /// </summary>
        CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Gets or sets the processor which is processing the data.
        /// </summary>
        Processor Processor { get; set; }
    }
}
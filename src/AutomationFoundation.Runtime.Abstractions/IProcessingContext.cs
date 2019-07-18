using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Identifies an object which provides contextual information for an item being processed by the runtime.
    /// </summary>
    public interface IProcessingContext
    {
        /// <summary>
        /// Gets the identifier of the item being processed.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the service scope.
        /// </summary>
        IServiceScope ServiceScope { get; }

        /// <summary>
        /// Gets the cancellation token to monitor for cancellation requests.
        /// </summary>
        CancellationToken CancellationToken { get; }
    }
}
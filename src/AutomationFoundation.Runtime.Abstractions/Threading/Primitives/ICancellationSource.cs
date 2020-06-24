using System;
using System.Threading;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    /// <summary>
    /// Identifies a source supporting cancellation of an operation.
    /// </summary>
    public interface ICancellationSource : IDisposable
    {
        /// <summary>
        /// Gets the cancellation token for the cancellation source.
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Gets a value indicating whether cancellation has been requested.
        /// </summary>
        bool IsCancellationRequested { get; }

        /// <summary>
        /// Requests the immediate cancellation of the operation.
        /// </summary>
        void RequestImmediateCancellation();

        /// <summary>
        /// Requests the cancellation of the operation after a delay.
        /// </summary>
        /// <param name="delay">The length of time to delay the cancellation request.</param>
        void RequestCancellationAfter(TimeSpan delay);
    }
}
using System.Threading;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    /// <summary>
    /// Identifies a mechanism for creating cancellation sources.
    /// </summary>
    public interface ICancellationSourceFactory
    {
        /// <summary>
        /// Creates a new cancellation source.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to use when linking the cancellation source.</param>
        /// <returns>The new cancellation source.</returns>
        ICancellationSource Create(CancellationToken cancellationToken);
    }
}
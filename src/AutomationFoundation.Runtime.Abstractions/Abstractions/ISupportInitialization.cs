using System.Threading;

namespace AutomationFoundation.Runtime.Abstractions
{
    /// <summary>
    /// Identifies an object which supports initialization.
    /// </summary>
    public interface ISupportInitialization
    {
        /// <summary>
        /// Initializes the object.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        void Initialize(CancellationToken cancellationToken);
    }
}
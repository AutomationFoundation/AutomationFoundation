using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.Abstractions.Synchronization;

/// <summary>
/// Identifies a synchronization policy which provides synchronized access to a protected resource.
/// </summary>
public interface ISynchronizationPolicy
{
    /// <summary>
    /// Acquires a lock on the resource.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to monitor while waiting for a lock on the resource.</param>
    /// <returns>The synchronization lock instance.</returns>
    Task<ISynchronizationLock> AcquireLockAsync(CancellationToken cancellationToken);
}
using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using AutomationFoundation.Runtime.Synchronization.Primitives;

namespace AutomationFoundation.Runtime.Synchronization.Policies;

/// <summary>
/// Provides a synchronization policy allowing one or more threads to access a single resource.
/// </summary>
public class OneOrMoreThreadsPerResourcePolicy : ISynchronizationPolicy, IDisposable
{
    private readonly SemaphoreSlim semaphore;
    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="OneOrMoreThreadsPerResourcePolicy"/> class.
    /// </summary>
    /// <param name="maximum">The maximum number of threads allowed to access the resource.</param>
    public OneOrMoreThreadsPerResourcePolicy(int maximum)
    {
        if (maximum <= 0)
        {
            throw new ArgumentException($"{nameof(maximum)} must be greater than zero.", nameof(maximum));
        }

        semaphore = new SemaphoreSlim(maximum, maximum);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="OneOrMoreThreadsPerResourcePolicy"/> class.
    /// </summary>
    ~OneOrMoreThreadsPerResourcePolicy()
    {
        Dispose(false);
    }

    /// <inheritdoc />
    public Task<ISynchronizationLock> AcquireLockAsync(CancellationToken cancellationToken)
    {
        GuardMustNotBeDisposed();

        return AcquireLockAsyncImpl(cancellationToken);
    }

    private async Task<ISynchronizationLock> AcquireLockAsyncImpl(CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync(cancellationToken);

        return new SemaphoreLock(semaphore);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources, otherwise false to release unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            semaphore.Dispose();
        }

        disposed = true;
    }

    /// <summary>
    /// Guards against the policy having been disposed.
    /// </summary>
    protected void GuardMustNotBeDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(OneOrMoreThreadsPerResourcePolicy));
        }
    }
}
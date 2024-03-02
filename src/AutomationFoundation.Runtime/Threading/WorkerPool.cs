using System;
using AutomationFoundation.Runtime.Abstractions.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using AutomationFoundation.Runtime.Threading.Internal;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading;

/// <summary>
/// Provides a pool of workers.
/// </summary>
public class WorkerPool : IWorkerPool, IDisposable
{
    private readonly IWorkerCacheMonitor cacheMonitor;
    private readonly IWorkerCache cache;

    private bool disposed;

    /// <summary>
    /// Creates a new worker pool.
    /// </summary>
    /// <returns>The worker pool.</returns>
    public static WorkerPool Create()
    {
        return Create(new WorkerPoolOptions
        {
            PollingInterval = TimeSpan.FromMinutes(1),
            MaximumDuration = TimeSpan.FromHours(1),
            Duration = TimeSpan.FromMinutes(5)
        });
    }

    /// <summary>
    /// Creates a new worker pool.
    /// </summary>
    /// <param name="options">The options to use when creating the pool.</param>
    /// <returns>The worker pool.</returns>
    public static WorkerPool Create(WorkerPoolOptions options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }
        else if (options.Duration <= TimeSpan.Zero)
        {
            throw new ArgumentException("The options must have a duration of greater than zero.", nameof(options));
        }
        else if (options.MaximumDuration <= TimeSpan.Zero || options.MaximumDuration < options.Duration)
        {
            throw new ArgumentException("The options must have a maximum duration of greater than zero, and must be greater than or equal to duration.", nameof(options));
        }
        else if (options.PollingInterval <= TimeSpan.Zero)
        {
            throw new ArgumentException("The options must have a polling interval of greater than zero.", nameof(options));
        }

        Timer timer = null;
        WorkerCache workerCache = null;
        WorkerCacheMonitor workerCacheMonitor = null;

        try
        {
            timer = new Timer();

            workerCache = new WorkerCache();
            workerCacheMonitor = new WorkerCacheMonitor(
                timer,
                workerCache,
                options);

            workerCacheMonitor.Start();

            return new WorkerPool(workerCache, workerCacheMonitor);
        }
        catch (Exception)
        {
            timer?.Dispose();
            workerCache?.Dispose();
            workerCacheMonitor?.Dispose();

            throw;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkerPool"/> class.
    /// </summary>
    /// <param name="cache">The cache of workers available for the pool.</param>
    /// <param name="cacheMonitor">The monitor which will track the cache.</param>
    internal WorkerPool(IWorkerCache cache, IWorkerCacheMonitor cacheMonitor)
    {
        this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        this.cacheMonitor = cacheMonitor ?? throw new ArgumentNullException(nameof(cacheMonitor));
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="WorkerPool"/> class.
    /// </summary>
    ~WorkerPool()
    {
        Dispose(false);
    }

    /// <inheritdoc />
    public IWorker Get(Action onRunCallback, Action postCompletedCallback)
    {
        if (onRunCallback == null)
        {
            throw new ArgumentNullException(nameof(onRunCallback));
        }

        GuardMustNotBeDisposed();

        IRuntimeWorker worker = null;

        try
        {
            worker = cache.Get();
            if (worker != null)
            {
                try
                {
                    worker.Initialize(new WorkerExecutionContext
                    {
                        OnRunCallback = onRunCallback,
                        PostCompletedCallback = postCompletedCallback
                    });

                    return CreatePooledWorker(worker);
                }
                catch (Exception)
                {
                    worker.Reset();
                    throw;
                }
            }
        }
        catch (Exception)
        {
            if (worker != null)
            {
                cache.Release(worker);
            }

            throw;
        }

        return null;
    }

    /// <summary>
    /// Creates a pooled worker.
    /// </summary>
    /// <param name="worker">The worker which is being managed by the pool.</param>
    /// <returns>The pooled worker instance.</returns>
    protected virtual IWorker CreatePooledWorker(IRuntimeWorker worker)
    {
        if (worker == null)
        {
            throw new ArgumentNullException(nameof(worker));
        }

        return new PooledWorker(cache, worker);
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
            cache.DisposeIfNecessary();
            cacheMonitor.DisposeIfNecessary();
        }

        disposed = true;
    }

    /// <summary>
    /// Guards against the pool having been disposed.
    /// </summary>
    protected void GuardMustNotBeDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(WorkerPool));
        }
    }
}
using System;

namespace AutomationFoundation.Runtime.Threading;

/// <summary>
/// Provides options for a worker pool.
/// </summary>
public class WorkerPoolOptions
{
    /// <summary>
    /// Gets or sets the interval in which to poll the cache for unused workers.
    /// </summary>
    public TimeSpan PollingInterval { get; set; }

    /// <summary>
    /// Gets or sets the maximum duration a worker may remain in the cache.
    /// </summary>
    public TimeSpan MaximumDuration { get; set; }

    /// <summary>
    /// Gets or sets the duration in which the worker must be issued to remain in the cache.
    /// </summary>
    public TimeSpan Duration { get; set; }
}
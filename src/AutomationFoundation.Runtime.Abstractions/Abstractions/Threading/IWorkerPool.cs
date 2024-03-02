using System;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Runtime.Abstractions.Threading;

/// <summary>
/// Identifies a pool of workers.
/// </summary>
public interface IWorkerPool
{
    /// <summary>
    /// Gets an available worker.
    /// </summary>
    /// <param name="onRunCallback">The action for the worker to execute.</param>
    /// <param name="postCompletedCallback">The action to execute after the <paramref name="onRunCallback" /> action.</param>
    /// <returns>The worker instance.</returns>
    IWorker Get(Action onRunCallback, Action postCompletedCallback);
}
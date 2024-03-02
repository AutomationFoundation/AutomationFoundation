using System;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

/// <summary>
/// Identifies a mechanism which can perform work.
/// </summary>
public interface IWorker : IDisposable
{
    /// <summary>
    /// Runs the worker.
    /// </summary>
    void Run();

    /// <summary>
    /// Runs the worker asynchronously.
    /// </summary>
    /// <returns>The task to await.</returns>
    Task RunAsync();

    /// <summary>
    /// Waits for the worker to complete.
    /// </summary>
    void WaitForCompletion();

    /// <summary>
    /// Waits for the worker to complete asynchronously.
    /// </summary>
    /// <returns>The task to await.</returns>
    Task WaitForCompletionAsync();
}
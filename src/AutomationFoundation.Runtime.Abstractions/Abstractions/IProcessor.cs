using System;

namespace AutomationFoundation.Runtime.Abstractions;

/// <summary>
/// Identifies a processor.
/// </summary>
public interface IProcessor : IDisposable
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the processor state.
    /// </summary>
    ProcessorState State { get; }

    /// <summary>
    /// Starts the processor.
    /// </summary>
    void Start();

    /// <summary>
    /// Stops the processor.
    /// </summary>
    void Stop();
}
using System;

namespace AutomationFoundation.Runtime;

/// <summary>
/// Provides a base implementation of an engine.
/// </summary>
public abstract class Engine : IDisposable
{
    private bool disposed;

    /// <summary>
    /// Finalizes the object instance.
    /// </summary>
    ~Engine()
    {
        Dispose(false);
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
        disposed = true;
    }

    /// <summary>
    /// Ensures the object has not been disposed.
    /// </summary>
    protected void GuardMustNotBeDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }
}
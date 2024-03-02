using System;

namespace AutomationFoundation.Runtime;

/// <summary>
/// Contains contextual information for an error which has occurred.
/// </summary>
public class ErrorHandlingContext
{
    private readonly Exception error;
    private bool handled;

    /// <summary>
    /// Gets the source of the error.
    /// </summary>
    public object Source { get; }

    /// <summary>
    /// Gets the severity of the error.
    /// </summary>
    public ErrorSeverityLevel Severity { get; }

    /// <summary>
    /// Initialize a new instance of the <see cref="ErrorHandlingContext"/> class.
    /// </summary>
    /// <param name="source">The source where the exception was caught.</param>
    /// <param name="severity">The severity of the error.</param>
    /// <param name="error">The error which occurred.</param>
    public ErrorHandlingContext(object source, ErrorSeverityLevel severity, Exception error)
    {
        Source = source ?? throw new ArgumentNullException(nameof(source));
        Severity = severity;
        this.error = error ?? throw new ArgumentNullException(nameof(error));
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="ErrorHandlingContext"/> class.
    /// </summary>
    protected ErrorHandlingContext()
    {
    }

    /// <summary>
    /// Gets the error.
    /// </summary>
    /// <returns>The error which occurred.</returns>
    public virtual Exception GetError()
    {
        return error;
    }

    /// <summary>
    /// Rethrows the error if it has not been handled.
    /// </summary>
    public virtual void RethrowErrorIfNotHandled()
    {
        if (handled)
        {
            return;
        }

        throw error;
    }

    /// <summary>
    /// Identifies that the error has been handled.
    /// </summary>
    public virtual void MarkErrorAsHandled()
    {
        handled = true;
    }
}
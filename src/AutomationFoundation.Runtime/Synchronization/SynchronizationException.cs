using System;
using System.Runtime.Serialization;

namespace AutomationFoundation.Runtime.Synchronization;

/// <summary>
/// Thrown when an exception occurs during synchronization.
/// </summary>
[Serializable]
public class SynchronizationException : RuntimeException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SynchronizationException"/> class.
    /// </summary>
    /// <param name="message">The message which describes the error.</param>
    public SynchronizationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SynchronizationException"/> class.
    /// </summary>
    /// <param name="message">The message which describes the error.</param>
    /// <param name="innerException">The exception which caused the current exception.</param>
    public SynchronizationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SynchronizationException"/> class.
    /// </summary>
    /// <param name="info">The object which holds the serialization</param>
    /// <param name="context">The context which holds streaming information about the source or destination.</param>
    protected SynchronizationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
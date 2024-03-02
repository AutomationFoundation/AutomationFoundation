using System;
using System.Runtime.Serialization;

namespace AutomationFoundation.Runtime;

/// <summary>
/// Thrown when an exception has occurred within the execution engine.
/// </summary>
[Serializable]
public class EngineException : RuntimeException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EngineException"/> class.
    /// </summary>
    /// <param name="message">The message which describes the error.</param>
    public EngineException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EngineException"/> class.
    /// </summary>
    /// <param name="message">The message which describes the error.</param>
    /// <param name="innerException">The exception which caused the current exception.</param>
    public EngineException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EngineException"/> class.
    /// </summary>
    /// <param name="info">The object which holds the serialization</param>
    /// <param name="context">The context which holds streaming information about the source or destination.</param>
    protected EngineException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
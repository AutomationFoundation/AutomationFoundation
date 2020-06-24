using System;
using System.Runtime.Serialization;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Thrown when an exception occurs while building an object.
    /// </summary>
    [Serializable]
    public class BuildException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildException"/> class.
        /// </summary>
        public BuildException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BuildException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception which is the cause of the current exception.</param>
        public BuildException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildException"/> class.
        /// </summary>
        /// <param name="info">The object which holds the serialization</param>
        /// <param name="context">The context which holds streaming information about the source or destination.</param>
        protected BuildException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
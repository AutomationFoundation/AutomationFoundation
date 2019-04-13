using System;
using System.Runtime.Serialization;

namespace AutomationFoundation.Builder
{
    /// <summary>
    /// Thrown when an exception occurs while validating the configuration of a builder.
    /// </summary>
    [Serializable]
    public class BuilderConfigurationException : BuildException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderConfigurationException"/> class.
        /// </summary>
        public BuilderConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderConfigurationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BuilderConfigurationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderConfigurationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception which is the cause of the current exception.</param>
        public BuilderConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderConfigurationException"/> class.
        /// </summary>
        /// <param name="info">The object which holds the serialization</param>
        /// <param name="context">The context which holds streaming information about the source or destination.</param>
        protected BuilderConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
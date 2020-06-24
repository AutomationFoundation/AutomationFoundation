using System;
using System.Runtime.Serialization;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Thrown when an exception has occurred by the runtime host.
    /// </summary>
    [Serializable]
    public class HostingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostingException"/> class.
        /// </summary>
        /// <param name="message">The message which describes the error.</param>
        public HostingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostingException"/> class.
        /// </summary>
        /// <param name="message">The message which describes the error.</param>
        /// <param name="innerException">The exception which caused the current exception.</param>
        public HostingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostingException"/> class.
        /// </summary>
        /// <param name="info">The object which holds the serialization</param>
        /// <param name="context">The context which holds streaming information about the source or destination.</param>
        protected HostingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
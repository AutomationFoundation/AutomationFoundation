using System;
using System.Runtime.Serialization;

namespace AutomationFoundation.Runtime.Threading
{
    /// <summary>
    /// Thrown when a task has been abandoned.
    /// </summary>
    [Serializable]
    public class TaskAbandonedException : OperationCanceledException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAbandonedException"/> class.
        /// </summary>
        public TaskAbandonedException()
            : this("The task has been abandoned.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAbandonedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public TaskAbandonedException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAbandonedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The exception which is the cause of this exception.</param>
        public TaskAbandonedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAbandonedException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected TaskAbandonedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
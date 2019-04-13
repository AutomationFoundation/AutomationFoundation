using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AutomationFoundation
{
    /// <summary>
    /// Thrown when an error occurs while validating an object.
    /// </summary>
    [Serializable]
    public class ValidationException : AggregateException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="innerExceptions">The inner exceptions that caused the validation exception.</param>
        public ValidationException(IEnumerable<Exception> innerExceptions)
            : base(innerExceptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="innerExceptions">The inner exceptions that caused the validation exception.</param>
        public ValidationException(params Exception[] innerExceptions)
            : base(innerExceptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="info">The object which holds the serialization</param>
        /// <param name="context">The context which holds streaming information about the source or destination.</param>
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
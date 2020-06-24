using System;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Identifies an object which handles errors which have occurred.
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        /// Handles the error.
        /// </summary>
        /// <param name="error">The error which occurred.</param>
        /// <param name="severity">The severity level of the error.</param>
        void Handle(Exception error, ErrorSeverityLevel severity);
    }
}
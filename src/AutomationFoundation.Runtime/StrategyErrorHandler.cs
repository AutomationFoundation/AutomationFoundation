using System;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Provides a mechanism to handle errors using an error handling strategy.
    /// </summary>
    public class StrategyErrorHandler : IErrorHandler
    {
        private readonly IErrorHandlingStrategy errorHandlingStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyErrorHandler"/> class.
        /// </summary>
        /// <param name="errorHandlingStrategy">The error handling strategy to handle the errors.</param>
        public StrategyErrorHandler(IErrorHandlingStrategy errorHandlingStrategy)
        {
            this.errorHandlingStrategy = errorHandlingStrategy ?? throw new ArgumentNullException(nameof(errorHandlingStrategy));
        }

        /// <inheritdoc />
        public void Handle(Exception error, ErrorSeverityLevel severity)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            var errorContext = new ErrorHandlingContext(this, severity, error);
            errorHandlingStrategy.Handle(errorContext);

            errorContext.RethrowErrorIfNotHandled();
        }
    }
}
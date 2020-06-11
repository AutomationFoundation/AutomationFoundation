using System;
using AutomationFoundation.Runtime;

namespace ConsoleRunner.Infrastructure.Diagnostics
{
    public class LogToConsoleErrorStrategy : IErrorHandlingStrategy
    {
        private readonly LoggingLevel level;

        public LogToConsoleErrorStrategy(LoggingLevel level)
        {
            this.level = level;
        }

        public void Handle(ErrorHandlingContext context)
        {
            if (context.Severity == ErrorSeverityLevel.NonFatal)
            {
                WriteWarning(context.GetError());
            }
            else
            {
                WriteError(context.GetError());
            }

            context.MarkErrorAsHandled();
        }

        private void WriteWarning(Exception error)
        {
            if (!ShouldLogLevel(LoggingLevel.Warning))
            {
                return;
            }

            WriteException(error);
        }

        private void WriteError(Exception error)
        {
            if (!ShouldLogLevel(LoggingLevel.Error))
            {
                return;
            }

            WriteException(error);
        }

        private void WriteException(Exception error)
        {
            Console.Out.WriteLine(error.ToString());
        }

        private bool ShouldLogLevel(LoggingLevel value)
        {
            return value <= level;
        }
    }
}
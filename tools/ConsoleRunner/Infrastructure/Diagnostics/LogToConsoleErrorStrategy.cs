using System;
using AutomationFoundation.Runtime;
using ConsoleRunner.Abstractions;

namespace ConsoleRunner.Infrastructure.Diagnostics
{
    public class LogToConsoleErrorStrategy : IErrorHandlingStrategy
    {
        private readonly IConsoleWriter writer;
        private readonly LoggingLevel level;

        public LogToConsoleErrorStrategy(IConsoleWriter writer, LoggingLevel level)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
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

            WriteException(error, ConsoleColor.Yellow);
        }

        private void WriteError(Exception error)
        {
            if (!ShouldLogLevel(LoggingLevel.Error))
            {
                return;
            }

            WriteException(error, ConsoleColor.Red);
        }

        private void WriteException(Exception error, ConsoleColor color)
        {
            WriteLine(error.ToString(), color);
        }

        private void WriteLine(string message, ConsoleColor color)
        {
            writer.WriteLine(message, color);
        }

        private bool ShouldLogLevel(LoggingLevel value)
        {
            return value <= level;
        }
    }
}
using System;

namespace ConsoleRunner.Infrastructure.IO;

public class ConsoleWriter : Abstractions.IConsoleWriter
{
    private static readonly object SyncRoot = new object();

    public void WriteEmptyLine()
    {
        lock (SyncRoot)
        {
            Console.WriteLine();
        }
    }

    public void WriteLine(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        lock (SyncRoot)
        {
            Console.WriteLine(text);
        }
    }

    public void WriteLine(string text, ConsoleColor foreColor)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        lock (SyncRoot)
        {
            var previousForecolor = Console.ForegroundColor;

            try
            {
                Console.ForegroundColor = foreColor;
                Console.WriteLine(text);
            }
            finally
            {
                Console.ForegroundColor = previousForecolor;
            }
        }
    }
}
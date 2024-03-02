using System;

namespace ConsoleRunner.Abstractions;

public interface IConsoleWriter
{
    void WriteEmptyLine();

    void WriteLine(string text);
    void WriteLine(string text, ConsoleColor foreColor);
}
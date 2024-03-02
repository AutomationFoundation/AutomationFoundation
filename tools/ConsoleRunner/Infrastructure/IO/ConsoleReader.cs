using System;

namespace ConsoleRunner.Infrastructure.IO;

public class ConsoleReader : Abstractions.IConsoleReader
{
    public void WaitForAnyKey()
    {
        Console.ReadKey(true);
    }
}
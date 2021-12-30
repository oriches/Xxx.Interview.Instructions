using System;
using System.Collections.Generic;

namespace Xxx.Interview.Instructions.Logging;

public sealed class ConsoleLogger : ILogger
{
    public static readonly ILogger Instance = new ConsoleLogger();
    private readonly List<string> _messages;

    private ConsoleLogger()
    {
        _messages = new List<string>();
    }

    public void Info()
    {
        _messages.Add(null);
    }

    public void Info(string message)
    {
        _messages.Add(message);
    }

    public void Flush()
    {
        foreach (var message in _messages)
            Console.WriteLine(message);

        _messages.Clear();
    }
}
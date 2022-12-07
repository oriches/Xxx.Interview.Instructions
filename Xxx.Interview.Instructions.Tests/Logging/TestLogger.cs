using System.Collections.Generic;
using Xxx.Interview.Instructions.Logging;

namespace Xxx.Interview.Instructions.Tests.Logging;

public sealed class TestLogger : ILogger
{
    public TestLogger() => Results = new List<string>();

    public List<string> Results { get; }

    public void Info() => Results.Add(string.Empty);

    public void Info(string message) => Results.Add(message);

    public void Flush()
    {
    }
}
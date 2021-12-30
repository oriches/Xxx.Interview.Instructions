namespace Xxx.Interview.Instructions.Logging;

public sealed class NullLogger : ILogger
{
    public static readonly ILogger Instance = new NullLogger();

    private NullLogger()
    {
    }

    public void Info()
    {
    }

    public void Info(string message)
    {
    }

    public void Flush()
    {
    }
}
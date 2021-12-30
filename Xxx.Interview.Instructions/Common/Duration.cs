using System;
using System.Diagnostics;
using Xxx.Interview.Instructions.Logging;

namespace Xxx.Interview.Instructions.Common;

public static class Duration
{
    public static ILogger Logger = NullLogger.Instance;

    public static bool IsEnabled = false;

    public static IDisposable Measure(Func<string> message)
    {
        if (!IsEnabled)
            return Disposable.Empty;

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        return Disposable.Create(() =>
        {
            stopwatch.Stop();
            Logger.Info($"{message()}, ticks=[{stopwatch.ElapsedTicks}], ms=[{stopwatch.ElapsedMilliseconds}]");
        });
    }
}
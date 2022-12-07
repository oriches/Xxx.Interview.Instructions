using System;

namespace Xxx.Interview.Instructions.Common;

public sealed class Disposable : IDisposable
{
    public static IDisposable Empty = new Disposable(null);

    private readonly Action _action;

    private Disposable(Action action) => _action = action;

    public void Dispose() => _action?.Invoke();

    public static IDisposable Create(Action action) => new Disposable(action);
}
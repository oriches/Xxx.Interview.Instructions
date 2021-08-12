using System;

namespace Xxx.Interview.Instructions.Operators
{
    public interface IOperator
    {
        string Name { get; }

        long Execute(params Func<long>[] operands);
    }
}
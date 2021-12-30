using System;
using System.Linq;

namespace Xxx.Interview.Instructions.Operators;

public sealed class MultipleOperator : Operator
{
    public MultipleOperator() : base("Mult")
    {
    }

    public override long Execute(params Func<long>[] operands)
    {
        return operands.Aggregate(1L, (a, b) => a * b());
    }
}
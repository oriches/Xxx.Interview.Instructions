using System;
using System.Linq;

namespace Xxx.Interview.Instructions.Operators;

public sealed class AddOperator : Operator
{
    public AddOperator() : base("Add")
    {
    }

    public override long Execute(params Func<long>[] operands)
    {
        return operands.Sum(op => op());
    }
}
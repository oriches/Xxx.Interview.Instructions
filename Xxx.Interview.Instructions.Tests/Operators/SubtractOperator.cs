using System;
using System.Linq;
using Xxx.Interview.Instructions.Operators;

namespace Xxx.Interview.Instructions.Tests.Operators
{
    public sealed class SubtractOperator : Operator
    {
        public SubtractOperator() : base("Sub")
        {
        }

        public override long Execute(params Func<long>[] operands)
        {
            var firstValue = operands.First()() * 2;
            return operands.Aggregate(firstValue, (a, b) => a - b());
        }
    }
}
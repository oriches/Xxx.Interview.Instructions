using System;

namespace Xxx.Interview.Instructions.Operators
{
    public sealed class ValueOperator : Operator
    {
        public ValueOperator() : base("Value")
        {
        }

        public override long Execute(params Func<long>[] operands)
        {
            if (operands.Length != 1)
                throw new Exception("Only one Operand expected!");

            return operands[0]();
        }
    }
}
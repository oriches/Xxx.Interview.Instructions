using System;
using System.Diagnostics;
using Xxx.Interview.Instructions.Operators;

namespace Xxx.Interview.Instructions;

[DebuggerDisplay("Identifier=[{Identifier}], Operator.Name=[{Operator.Name}]")]
public sealed class Instruction : IEquatable<Instruction>
{
    private long? _cachedResult;

    public Instruction(int identifier, IOperator @operator, params Func<long>[] operands)
    {
        Identifier = identifier;
        Operator = @operator;
        Operands = operands;
    }

    public int Identifier { get; }

    public IOperator Operator { get; }

    public Func<long>[] Operands { get; }

    public bool Equals(Instruction other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Identifier == other.Identifier;
    }

    public override bool Equals(object obj) =>
        ReferenceEquals(this, obj) || (obj is Instruction other && Equals(other));

    public override int GetHashCode() => Identifier;

    public static bool operator ==(Instruction left, Instruction right) => Equals(left, right);

    public static bool operator !=(Instruction left, Instruction right) => !Equals(left, right);

    public long Execute()
    {
        if (_cachedResult != null)
            return _cachedResult.Value;

        var result = Operator.Execute(Operands);
        _cachedResult = result;

        return result;
    }
}
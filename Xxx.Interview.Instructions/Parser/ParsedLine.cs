using System;
using System.Diagnostics;

namespace Xxx.Interview.Instructions.Parser;

[DebuggerDisplay("Identifier=[{Identifier}], OperatorName=[{OperatorName}]")]
public sealed class ParsedLine : IEquatable<ParsedLine>
{
    public ParsedLine(int identifier, string operatorName, int[] operands)
    {
        Identifier = identifier;
        OperatorName = string.Intern(operatorName);
        Operands = operands;
    }

    public int Identifier { get; }

    public string OperatorName { get; }

    public int[] Operands { get; }

    public bool Equals(ParsedLine other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Identifier == other.Identifier;
    }

    public override bool Equals(object obj)
    {
        return ReferenceEquals(this, obj) || obj is ParsedLine other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Identifier.GetHashCode();
    }

    public static bool operator ==(ParsedLine left, ParsedLine right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ParsedLine left, ParsedLine right)
    {
        return !Equals(left, right);
    }
}
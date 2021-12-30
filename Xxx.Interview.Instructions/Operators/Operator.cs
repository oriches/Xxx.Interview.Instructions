using System;

namespace Xxx.Interview.Instructions.Operators;

public abstract class Operator : IOperator, IEquatable<Operator>
{
    protected Operator(string name)
    {
        Name = name;
    }

    public bool Equals(Operator other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public string Name { get; }

    public abstract long Execute(params Func<long>[] operands);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Operator)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public static bool operator ==(Operator left, Operator right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Operator left, Operator right)
    {
        return !Equals(left, right);
    }
}
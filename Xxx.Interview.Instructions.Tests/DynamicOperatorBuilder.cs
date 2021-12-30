using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Xxx.Interview.Instructions.Tests;

public static class DynamicOperatorBuilder
{
    public static void CreateDuplicate()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(@"using System;
            using System.Linq;
            using Xxx.Interview.Instructions.Operators;

            namespace Xxx.Interview.Instructions.Tests.Operators
            {
                public sealed class DuplicateOperator : Operator
                {
                    public DuplicateOperator() : base(""Sub"")
                    {
                    }

                    public override long Execute(params Func<long>[] operands)
                    {
                        var firstValue = operands.First()() * 2;
                        return operands.Aggregate(firstValue, (a, b) => a - b());
                    }
                }
            }");

        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic)
            .Select(a => a.Location)
            .Distinct()
            .Select(l => MetadataReference.CreateFromFile(l))
            .ToArray();

        var compilation = CSharpCompilation.Create("Xxx.Interview.Instructions.DynamicOperator", new[] { syntaxTree },
            references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);
        if (result.Success)
        {
            ms.Seek(0, SeekOrigin.Begin);
            Assembly.Load(ms.ToArray());
        }
    }
}
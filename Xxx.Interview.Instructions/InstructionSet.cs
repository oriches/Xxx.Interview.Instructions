using System;
using System.Collections.Generic;
using System.Linq;
using Xxx.Interview.Instructions.Common;
using Xxx.Interview.Instructions.Operators;
using Xxx.Interview.Instructions.Parser;

namespace Xxx.Interview.Instructions
{
    public sealed class InstructionSet
    {
        private readonly IParser _instructionParser;
        private readonly HashSet<IOperator> _operators;

        private Dictionary<int, Instruction> _instructions;

        public InstructionSet(IOperatorFactory factory, IParser instructionParser)
        {
            _instructionParser = instructionParser;
            _operators = factory.Operators.ToHashSet();

            _instructions = new Dictionary<int, Instruction>();
        }

        public void LoadFromFile(string file)
        {
            using (Duration.Measure(() => "Building Instructions"))
            {
                _instructions = _instructionParser.ParseFile(file)
                    .Select(parsedLine =>
                    {
                        var @operator = FindOperator(parsedLine.OperatorName);
                        var operands = parsedLine.Operands.Select(operand =>
                                @operator is ValueOperator ? () => operand : (Func<long>) (() => Execute(operand)))
                            .ToArray();

                        return new Instruction(parsedLine.Identifier, @operator, operands);
                    })
                    .ToDictionary(instruction => instruction.Identifier, instruction => instruction);
            }
        }

        public long Execute(int identifier)
        {
            using var measure = Duration.Measure(() => $"Executing Instruction, identifier=[{identifier}]");
            if (!_instructions.TryGetValue(identifier, out var instruction))
                throw new Exception($"Failed to find identifier, value=[{identifier}]");
            return instruction.Execute();
        }

        private IOperator FindOperator(string name)
        {
            var @operator = _operators.FirstOrDefault(op =>
                string.Equals(op.Name, name, StringComparison.InvariantCultureIgnoreCase));

            return @operator ?? throw new Exception($"Unknown Operator, Name=[{name}]");
        }
    }
}
using System;
using Xxx.Interview.Instructions.Common;
using Xxx.Interview.Instructions.Logging;
using Xxx.Interview.Instructions.Operators;
using Xxx.Interview.Instructions.Parser;

namespace Xxx.Interview.Instructions
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Duration.Logger = ConsoleLogger.Instance;
            Duration.IsEnabled = true;

            using (Duration.Measure(() => "Total Execution Time"))
            {
                var instructionSet =
                    new InstructionSet(new OperatorFactory(ConsoleLogger.Instance), new InstructionParser());

                instructionSet.LoadFromFile("input.txt");

                ConsoleLogger.Instance.Info($"Answer=[{instructionSet.Execute(100):N0}]");
                ConsoleLogger.Instance.Info();
            }

            ConsoleLogger.Instance.Flush();

            Console.WriteLine();
            Console.WriteLine("Press ENTER to Close...");
            Console.ReadLine();
        }
    }
}
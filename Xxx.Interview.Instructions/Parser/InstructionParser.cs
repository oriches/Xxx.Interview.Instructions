using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xxx.Interview.Instructions.Common;

namespace Xxx.Interview.Instructions.Parser;

public sealed class InstructionParser : IParser
{
    private static readonly Regex OperatorRegex = new(@"[a-zA-Z]", RegexOptions.Compiled);

    private static readonly char[] PossibleSeparators =
    {
        ';', ':', ',', '/', '\\', '|', ' '
    };

    private static readonly char[] PossibleIdentifierSuffix =
    {
        ':', ';', '|', '>'
    };

    public ParsedLine[] ParseFile(string file)
    {
        var results = new List<ParsedLine>();

        using (Duration.Measure(() => "Parsing File"))
        {
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException(nameof(file));

            if (!File.Exists(file))
                throw new Exception($"File does not exist, file=[{file}]");

            using var stream = File.OpenRead(file);
            using var streamReader = new StreamReader(stream, Encoding.UTF8);
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    var tokens = SplitOnSeparator(line)
                        .ToList();

                    var identifierString = tokens.FirstOrDefault(t => PossibleIdentifierSuffix.Contains(t.Last()));
                    if (identifierString == null)
                        throw new Exception("Failed to find Identifier!");

                    tokens.Remove(identifierString);

                    var identifier = Convert.ToInt32(identifierString.Remove(identifierString.Length - 1));

                    var operatorName = tokens.FirstOrDefault(t => OperatorRegex.IsMatch(t));

                    tokens.Remove(operatorName);

                    var operands = tokens.Select(t =>
                        {
                            var trimmed = t.Trim();
                            return Convert.ToInt32(trimmed);
                        })
                        .ToArray();

                    results.Add(new ParsedLine(identifier, operatorName, operands));
                }
            }
        }

        return results.OrderBy(pl => pl.Identifier)
            .ToArray();
    }

    private static IEnumerable<string> SplitOnSeparator(string line)
    {
        var possible = PossibleSeparators.Select(ps => line.Split(ps)
            .Where(t => !string.IsNullOrEmpty(t))
            .ToArray());

        return possible.OrderByDescending(p => p.Length)
            .First();
    }
}
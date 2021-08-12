using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xxx.Interview.Instructions.Common;
using Xxx.Interview.Instructions.Logging;

namespace Xxx.Interview.Instructions.Operators
{
    public sealed class OperatorFactory : IOperatorFactory
    {
        private readonly ILogger _logger;
        private readonly IOperator[] _operators;

        public OperatorFactory(ILogger logger)
        {
            using (Duration.Measure(() => "Finding & Loading Operators"))
            {
                _logger = logger;

                var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                    .Location);

                if (directory != null)
                {
                    var operators = new List<IOperator>();

                    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => !a.IsDynamic && a.FullName != null && a.FullName.StartsWith("Xxx.Interview."))
                        .ToList();

                    var assemblyLocations = assemblies.Select(a => a.Location)
                        .ToArray();

                    var files = Directory.GetFiles(directory, "Xxx.Interview.*.dll");
                    assemblies.AddRange(files.Where(file => !assemblyLocations.Contains(file))
                        .Select(Assembly.LoadFile));

                    foreach (var assembly in assemblies)
                        try
                        {
                            var instances = assembly.GetTypes()
                                .Where(type => !type.IsAbstract && type.IsClass && type.IsPublic && type.GetInterfaces()
                                    .Contains(typeof(IOperator)))
                                .Select(Create)
                                .ToArray();

                            operators.AddRange(instances);
                        }
                        catch (Exception)
                        {
                            // swallow and move on...
                        }

                    var duplicateNames = operators.GroupBy(@operator => @operator.Name)
                        .Where(grouping => grouping.Count() > 1)
                        .Select(grouping => grouping.Key)
                        .ToArray();

                    if (duplicateNames.Any())
                        throw new Exception($"Duplicate Operator defined, Name=[{duplicateNames.First()}]");

                    _operators = operators.ToArray();
                }
                else
                {
                    _operators = Array.Empty<IOperator>();
                }
            }
        }

        public IOperator[] Operators => _operators.ToArray();

        private IOperator Create(Type type)
        {
            _logger.Info($"Creating Operator, Type=[{type.UnderlyingSystemType.FullName}]");

            // simple creation because examples all have parameter-less constructors
            var @operator = (IOperator) Activator.CreateInstance(type.UnderlyingSystemType);

            return @operator;
        }
    }
}
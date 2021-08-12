using System;
using System.Linq;
using NUnit.Framework;
using Xxx.Interview.Instructions.Logging;
using Xxx.Interview.Instructions.Operators;
using Xxx.Interview.Instructions.Tests.Operators;

namespace Xxx.Interview.Instructions.Tests
{
    [TestFixture]
    public class OperatorFactoryTests
    {
        [Test]
        public void discovers_all_operators()
        {
            // arrange
            var operatorFactory = new OperatorFactory(ConsoleLogger.Instance);

            // act
            var operators = operatorFactory.Operators.ToArray();

            // assert
            Assert.That(operators.Length, Is.EqualTo(4));
            Assert.That(operators.Select(x => x.GetType()
                .FullName), Contains.Item(typeof(SubtractOperator).FullName));
        }

        [Test]
        public void throws_exception_when_there_are_duplicate_operators()
        {
            // arrange
            DynamicOperatorBuilder.CreateDuplicate();

            // act
            Exception expected = null;
            try
            {
                new OperatorFactory(ConsoleLogger.Instance);
            }
            catch (Exception exception)
            {
                expected = exception;
            }

            // assert
            Assert.That(expected, Is.Not.Null);
        }
    }
}
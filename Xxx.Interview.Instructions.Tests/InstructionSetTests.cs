using System;
using Moq;
using NUnit.Framework;
using Xxx.Interview.Instructions.Common;
using Xxx.Interview.Instructions.Operators;
using Xxx.Interview.Instructions.Parser;
using Xxx.Interview.Instructions.Tests.Logging;

namespace Xxx.Interview.Instructions.Tests
{
    [TestFixture]
    public class InstructionSetTests
    {
        [SetUp]
        public void SetUp()
        {
            _operatorFactory = new Mock<IOperatorFactory>();
            _operatorFactory.Setup(of => of.Operators)
                .Returns(new IOperator[]
                {
                    new AddOperator(),
                    new MultipleOperator(),
                    new ValueOperator()
                });

            _parser = new InstructionParser();
        }

        private InstructionParser _parser;
        private Mock<IOperatorFactory> _operatorFactory;

        [Test]
        public void exception_when_unknown_operator()
        {
            // arrange
            var @is = new InstructionSet(_operatorFactory.Object, _parser);

            // act
            Exception expected = null;
            try
            {
                @is.LoadFromFile("data/unknown_operator.txt");
            }
            catch (Exception exception)
            {
                expected = exception;
            }

            // assert
            Assert.That(expected, Is.Not.Null);
        }

        [Test]
        public void exception_when_invalid_operand()
        {
            // arrange
            var @is = new InstructionSet(_operatorFactory.Object, _parser);
            @is.LoadFromFile("data/invalid_operand.txt");

            // act
            Exception expected = null;
            try
            {
                @is.Execute(222);
            }
            catch (Exception exception)
            {
                expected = exception;
            }

            // assert
            Assert.That(expected, Is.Not.Null);
        }

        [Test]
        public void logger_is_enabled()
        {
            // arrange
            var testLogger = new TestLogger();
            Duration.Logger = testLogger;
            Duration.IsEnabled = true;

            var @is = new InstructionSet(_operatorFactory.Object, _parser);
            @is.LoadFromFile("data/add.txt");

            // act
            @is.Execute(1);

            // assert
            Assert.That(testLogger.Results, Is.Not.Empty);
        }

        [Test]
        public void instruction_example()
        {
            // arrange
            var @is = new InstructionSet(_operatorFactory.Object, _parser);
            @is.LoadFromFile("data/instruction_example.txt");

            // act
            var result = @is.Execute(0);

            // assert
            Assert.That(result, Is.EqualTo(4));
        }
    }
}
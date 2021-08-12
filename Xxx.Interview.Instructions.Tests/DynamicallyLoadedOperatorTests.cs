using Moq;
using NUnit.Framework;
using Xxx.Interview.Instructions.Operators;
using Xxx.Interview.Instructions.Parser;
using Xxx.Interview.Instructions.Tests.Operators;

namespace Xxx.Interview.Instructions.Tests
{
    [TestFixture]
    public class DynamicallyLoadedOperatorTests
    {
        [SetUp]
        public void SetUp()
        {
            _operatorFactory = new Mock<IOperatorFactory>();
            _operatorFactory.Setup(of => of.Operators)
                .Returns(new IOperator[]
                {
                    new AddOperator(),
                    new SubtractOperator(),
                    new ValueOperator()
                });

            _parser = new InstructionParser();
        }

        private InstructionParser _parser;
        private Mock<IOperatorFactory> _operatorFactory;

        [Test]
        public void Subtract_positive_values()
        {
            // arrange
            var @is = new InstructionSet(_operatorFactory.Object, _parser);
            @is.LoadFromFile("data/subtract_positive.txt");

            // act
            var answer = @is.Execute(1);

            // assert
            Assert.That(answer, Is.EqualTo(5));
        }

        [Test]
        public void Subtract_negative_values()
        {
            // arrange
            var @is = new InstructionSet(_operatorFactory.Object, _parser);
            @is.LoadFromFile("data/subtract_negative.txt");

            // act
            var answer = @is.Execute(1);

            // assert
            Assert.That(answer, Is.EqualTo(-2));
        }
    }
}
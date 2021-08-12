using Moq;
using NUnit.Framework;
using Xxx.Interview.Instructions.Operators;
using Xxx.Interview.Instructions.Parser;

namespace Xxx.Interview.Instructions.Tests
{
    [TestFixture]
    public class AddOperatorTests
    {
        [SetUp]
        public void SetUp()
        {
            _operatorFactory = new Mock<IOperatorFactory>();
            _operatorFactory.Setup(of => of.Operators)
                .Returns(new IOperator[]
                {
                    new AddOperator(),
                    new ValueOperator()
                });

            _parser = new InstructionParser();
        }

        private InstructionParser _parser;
        private Mock<IOperatorFactory> _operatorFactory;

        [Test]
        public void add()
        {
            // arrange
            var @is = new InstructionSet(_operatorFactory.Object, _parser);
            @is.LoadFromFile("data/add.txt");

            // act
            var answer = @is.Execute(1);

            // assert
            Assert.That(answer, Is.EqualTo(9));
        }
    }
}
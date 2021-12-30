using Moq;
using NUnit.Framework;
using Xxx.Interview.Instructions.Operators;
using Xxx.Interview.Instructions.Parser;

namespace Xxx.Interview.Instructions.Tests;

[TestFixture]
public class MultipleOperatorTests
{
    [SetUp]
    public void SetUp()
    {
        _operatorFactory = new Mock<IOperatorFactory>();
        _operatorFactory.Setup(of => of.Operators)
            .Returns(new IOperator[]
            {
                new MultipleOperator(),
                new ValueOperator()
            });

        _parser = new InstructionParser();
    }

    private InstructionParser _parser;
    private Mock<IOperatorFactory> _operatorFactory;

    [Test]
    public void multiple()
    {
        // arrange
        var @is = new InstructionSet(_operatorFactory.Object, _parser);
        @is.LoadFromFile("data/multiple.txt");

        // act
        var answer = @is.Execute(1);

        // assert
        Assert.That(answer, Is.EqualTo(6));
    }
}
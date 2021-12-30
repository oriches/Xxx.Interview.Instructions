using System;
using Moq;
using NUnit.Framework;
using Xxx.Interview.Instructions.Operators;
using Xxx.Interview.Instructions.Parser;

namespace Xxx.Interview.Instructions.Tests;

[TestFixture]
public class ValueOperatorTests
{
    [SetUp]
    public void SetUp()
    {
        _operatorFactory = new Mock<IOperatorFactory>();
        _operatorFactory.Setup(of => of.Operators)
            .Returns(new IOperator[]
            {
                new ValueOperator()
            });

        _parser = new InstructionParser();
    }

    private InstructionParser _parser;
    private Mock<IOperatorFactory> _operatorFactory;

    [Test]
    public void value()
    {
        // arrange
        var @is = new InstructionSet(_operatorFactory.Object, _parser);
        @is.LoadFromFile("data/value.txt");

        // act
        // assert
        Assert.That(@is.Execute(222), Is.EqualTo(10));
        Assert.That(@is.Execute(223), Is.EqualTo(-3));
        Assert.That(@is.Execute(224), Is.EqualTo(11));
    }

    [Test]
    public void exception_when_multiple_operands()
    {
        // arrange
        var @is = new InstructionSet(_operatorFactory.Object, _parser);
        @is.LoadFromFile("data/value.txt");

        // act
        Exception expected = null;
        try
        {
            @is.Execute(225);
        }
        catch (Exception exception)
        {
            expected = exception;
        }

        // assert
        Assert.That(expected, Is.Not.Null);
    }
}
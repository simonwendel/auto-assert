namespace AutoAssert.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoAssert;
    using AutoFixture.Kernel;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class ParameterListBuilderTests
    {
        private readonly Mock<ISpecimenContext> context;
        private readonly ParameterListBuilder sut;

        public ParameterListBuilderTests()
        {
            context = new Mock<ISpecimenContext>();
            sut = new ParameterListBuilder(context.Object);
        }

        [Fact]
        public void Constructor_GivenNullFixture_ThrowsException()
        {
            Action constructing = () => new ParameterListBuilder(null);
            constructing.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("context");
        }

        [Fact]
        public void GetNullCombinations_GivenNullParametersArray_ThrowsException()
        {
            Action getting = () => sut.GetNullCombinations(null).ToList();
            getting.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("parameters");
            context.Verify(f => f.Resolve(It.IsAny<Type>()), Times.Never);
        }

        [Fact]
        public void GetNullCombinations_GivenEmptyParametersArray_ReturnsEmptyParameterLists()
        {
            var parameters = new ParameterInfo[0];
            sut.GetNullCombinations(parameters).Should().BeEmpty();
        }

        [Fact]
        public void GetNullCombinations_GivenValueTypeParameters_ReturnsEmptyParameterLists()
        {
            Action<int, float, double> onlyValueTypes = (i, s, v) => { };
            var parameters = onlyValueTypes.Method.GetParameters();
            sut.GetNullCombinations(parameters).Should().BeEmpty();
            context.Verify(c => c.Resolve(It.IsAny<Type>()), Times.Never);
        }

        [Fact]
        public void GetNullCombinations_GivenReferenceTypeParameters_ReturnsResolvedParameterListsWithNulls()
        {
            Action<object, int, string> someReferences = (o, i, s) => { };
            var parameters = someReferences.Method.GetParameters();

            var obj = new object();
            var number = 1337;
            var str = "somestring";

            context.Setup(c => c.Resolve(typeof(object))).Returns(obj);
            context.Setup(c => c.Resolve(typeof(int))).Returns(number);
            context.Setup(c => c.Resolve(typeof(string))).Returns(str);

            var combinations = new List<(string, object[])>
            {
                ("o", new object[] { null, number, str }),
                ("s", new object[] { obj, number, null })
            };

            sut.GetNullCombinations(parameters).Should().BeEquivalentTo(combinations);
        }
    }
}

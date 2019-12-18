namespace AutoAssert.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoAssert.Tests.TestTypes;
    using AutoFixture;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class NullCheckAssertionsTests
    {
        private readonly object checkedObject;
        private readonly MethodBase method;
        private readonly ParameterInfo[] parameters;
        private readonly IEnumerable<(string, object[])> combos;
        private readonly Mock<IParameterListBuilder> paramBuilder;
        private readonly Mock<IMemberFinder> memberFinder;
        private readonly Mock<IMethodInvoker> methodInvoker;
        private readonly NullCheckAsserter sut;

        public NullCheckAssertionsTests()
        {
            var fixture = new Fixture();
            checkedObject = fixture.Create<object>();

            method = ReflectionValueFactory.GetMethod();
            parameters = method.GetParameters();

            combos = fixture.CreateMany<(string, object[])>(10);

            paramBuilder = new Mock<IParameterListBuilder>();
            paramBuilder.Setup(p => p.GetNullCombinations(parameters)).Returns(combos);

            memberFinder = new Mock<IMemberFinder>();
            methodInvoker = new Mock<IMethodInvoker>();

            sut = new NullCheckAsserter(paramBuilder.Object, memberFinder.Object, methodInvoker.Object);
        }

        [Fact]
        public void Constructor_GivenNullParameterListBuilder_ThrowsException()
        {
            Action constructing = () => new NullCheckAsserter(null, memberFinder.Object, methodInvoker.Object);
            constructing.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("paramBuilder");
        }

        [Fact]
        public void Constructor_GivenNullMemberFinder_ThrowsException()
        {
            Action constructing = () => new NullCheckAsserter(paramBuilder.Object, null, methodInvoker.Object);
            constructing.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("memberFinder");
        }

        [Fact]
        public void Constructor_GivenNullMethodInvoker_ThrowsException()
        {
            Action constructing = () => new NullCheckAsserter(paramBuilder.Object, memberFinder.Object, null);
            constructing.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("methodInvoker");
        }

        [Fact]
        public void NullCheckParameters_GivenNullSystemUnderTest_ThrowsException()
        {
            Action checking = () => sut.NullCheckParameters(null, method);
            checking.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("systemUnderTest");
        }

        [Fact]
        public void NullCheckParameters_GivenNullMethod_ThrowsException()
        {
            Action checking = () => sut.NullCheckParameters(checkedObject, null);
            checking.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("method");
        }

        [Fact]
        public void NullCheckParameters_GivenMethod_BuildsParamComboList()
        {
            sut.NullCheckParameters(checkedObject, method);
            paramBuilder.Verify(p => p.GetNullCombinations(parameters), Times.Once);
        }

        [Fact]
        public void NullCheckParameters_GivenMethod_InvokesMethodOnAllPossibleParamLists()
        {
            sut.NullCheckParameters(checkedObject, method);
            foreach (var (paramName, paramList) in combos)
            {
                methodInvoker.Verify(m => m.Invoke(method, checkedObject, paramList), Times.Once);
            }

            methodInvoker.VerifyNoOtherCalls();
        }

        [Fact]
        public void NullCheckParameters_GivenMethod_CollectsParamCombosThatDoesntThrow()
        {
            var shouldThrow = combos.Where((c, i) => i % 2 == 0);
            foreach (var (_, paramList) in shouldThrow)
            {
                var invokationException = new TargetInvocationException(new ArgumentNullException());
                methodInvoker.Setup(m => m.Invoke(method, checkedObject, paramList)).Throws(invokationException);
            }

            var shouldntThrow = combos.Where(c => !shouldThrow.Contains(c));
            var namesOfThoseThatDontThrow = shouldntThrow.Select(c => c.Item1);

            sut.NullCheckParameters(checkedObject, method).Should().BeEquivalentTo(namesOfThoseThatDontThrow);
        }
    }
}

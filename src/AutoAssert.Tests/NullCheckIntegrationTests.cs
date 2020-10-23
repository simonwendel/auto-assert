namespace AutoAssert.Tests
{
    using System;
    using System.Linq;
    using AutoAssert;
    using AutoAssert.Tests.TestTypes;
    using FluentAssertions;
    using Xunit;

    public class NullCheckIntegrationTests
    {
        private readonly NullCheckAsserter assert;

        public NullCheckIntegrationTests()
        {
            assert = Asserters.GetNullCheckAsserter();
        }

        [Fact]
        public void AllCheckedIn_GivenTypeThatChecksAllPublicParameters_DoesNotThrow()
        {
            Action checkingForNull = () => assert.AllCheckedIn<ClassThatChecksAllForNull>();
            checkingForNull.Should().NotThrow(because: "all parameters in all methods and ctors are checked");
        }

        [Fact]
        public void AllCheckedIn_GivenTypeThatDoesNotCheckAllPublicParameters_ThrowsException()
        {
            var expectedExceptionSequence = new Exception[]
            {
                new ConstructorCheckMissingException("abstractDependency"),
                new MethodCheckMissingException("set_PropertyWithOutChecks", "value"),
                new MethodCheckMissingException("UnitOfWork1", "param1"),
                new MethodCheckMissingException("UnitOfWork2", "param3")
            };

            Action notCheckingForNull = () => assert.AllCheckedIn<ClassThatDoesntCheckAllForNull>();
            notCheckingForNull.Should().ThrowExactly<AggregateException>(because: "not all parameters are checked")
                .Where(exc => exc.InnerExceptions.SequenceEqual(expectedExceptionSequence));
        }

        [Fact]
        public void AllCheckedIn_GivenTypeWithRefParametersThatChecksForNulls_DoesNotThrow()
        {
            assert.AllCheckedIn<ClassWithRefParameters>();
        }

        [Fact]
        public void AllCheckedIn_GivenTypeWithOutParametersThatChecksForNulls_DoesNotThrow()
        {
            assert.AllCheckedIn<ClassWithOutParameters>();
        }

        [Fact]
        public void AllCheckedIn_GivenGenericTypeOfValueTypeThatChecksForNulls_DoesNotThrow()
        {
            assert.AllCheckedIn<GenericClass<int>>();
        }

        [Fact]
        public void AllCheckedIn_GivenGenericTypeOfObjectThatChecksForNulls_DoesNotThrow()
        {
            assert.AllCheckedIn<GenericClass<object>>();
        }

        [Fact]
        public void AllCheckedIn_GivenGenericTypeOfStringThatChecksForNulls_DoesNotThrow()
        {
            assert.AllCheckedIn<GenericClass<string>>();
        }

        [Fact]
        public void AllCheckedIn_GivenGenericTypeOfInterfaceThatChecksForNulls_DoesNotThrow()
        {
            assert.AllCheckedIn<GenericClass<Dependencies.ISomeDependency>>();
        }
    }
}

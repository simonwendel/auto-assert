namespace AutoVerify.Tests
{
    using System;
    using AutoFixture;
    using FluentAssertions;
    using Xunit;

    public class MethodCheckMissingExceptionTests
    {
        private readonly string methodName;
        private readonly string otherMethodName;
        private readonly string paramName;
        private readonly string otherParamName;
        private readonly MethodCheckMissingException sut;

        public MethodCheckMissingExceptionTests()
        {
            var fixture = new Fixture();

            methodName = fixture.Create<string>();
            otherMethodName = fixture.Create<string>();

            paramName = fixture.Create<string>();
            otherParamName = fixture.Create<string>();

            sut = new MethodCheckMissingException(methodName, paramName);
        }

        [Fact]
        public void Constructor_GivenNullMethodName_DoesNotThrow()
        {
            Action constructing = () => new MethodCheckMissingException(null, paramName);
            constructing.Should().NotThrow();
        }

        [Fact]
        public void Constructor_GivenNullParamName_DoesNotThrow()
        {
            Action constructing = () => new MethodCheckMissingException(methodName, null);
            constructing.Should().NotThrow();
        }

        [Fact]
        public void Constructor_GivenMethodName_SetsMethodNameProperty()
        {
            sut.MethodName.Should().Be(methodName);
        }

        [Fact]
        public void Constructor_GivenParamName_SetsParamNameProperty()
        {
            sut.ParamName.Should().Be(paramName);
        }

        [Fact]
        public void Equals_GivenNull_ReturnsFalse()
        {
            sut.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void Equals_GivenWrongType_ReturnsFalse()
        {
            sut.Equals(new object()).Should().BeFalse();
        }

        [Fact]
        public void Equals_GivenOtherMethodName_ReturnsFalse()
        {
            var otherException = new MethodCheckMissingException(otherMethodName, paramName);
            sut.Equals(otherException).Should().BeFalse();
        }

        [Fact]
        public void Equals_GivenOtherParamName_ReturnsFalse()
        {
            var otherException = new MethodCheckMissingException(methodName, otherParamName);
            sut.Equals(otherException).Should().BeFalse();
        }

        [Fact]
        public void Equals_GivenSameMethodNameAndParamName_ReturnsTrue()
        {
            var otherException = new MethodCheckMissingException(methodName, paramName);
            sut.Equals(otherException).Should().BeTrue();
        }

        [Fact]
        public void Equals_GivenSameObject_ReturnsTrue()
        {
            sut.Equals(sut).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_WhenMethodNameDiffers_ReturnsDifferentHashCode()
        {
            var otherException = new MethodCheckMissingException(otherMethodName, paramName);
            sut.GetHashCode().Should().NotBe(otherException.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenParamNameDiffers_ReturnsDifferentHashCode()
        {
            var otherException = new MethodCheckMissingException(methodName, otherParamName);
            sut.GetHashCode().Should().NotBe(otherException.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenSameMethodNameAndParamName_ReturnsSameHashCode()
        {
            var otherException = new MethodCheckMissingException(methodName, paramName);
            sut.GetHashCode().Should().Be(otherException.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenCalledOnSameObject_ReturnsSameHashCode()
        {
            sut.GetHashCode().Should().Be(sut.GetHashCode());
        }
    }
}

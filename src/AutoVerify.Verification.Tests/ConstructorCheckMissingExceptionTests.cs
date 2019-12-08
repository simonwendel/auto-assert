namespace AutoVerify.Verification.Tests
{
    using System;
    using AutoFixture;
    using AutoVerify.Verification;
    using FluentAssertions;
    using Xunit;

    public class ConstructorCheckMissingExceptionTests
    {
        private readonly string paramName;
        private readonly string otherParamName;
        private readonly ConstructorCheckMissingException sut;

        public ConstructorCheckMissingExceptionTests()
        {
            var fixture = new Fixture();
            paramName = fixture.Create<string>();
            otherParamName = fixture.Create<string>();
            sut = new ConstructorCheckMissingException(paramName);
        }

        [Fact]
        public void Constructor_GivenNullParamName_DoesNotThrow()
        {
            Action constructing = () => new ConstructorCheckMissingException(null);
            constructing.Should().NotThrow();
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
        public void Equals_GivenOtherParamName_ReturnsFalse()
        {
            var otherException = new ConstructorCheckMissingException(otherParamName);
            sut.Equals(otherException).Should().BeFalse();
        }

        [Fact]
        public void Equals_GivenSameParamName_ReturnsTrue()
        {
            var otherException = new ConstructorCheckMissingException(paramName);
            sut.Equals(otherException).Should().BeTrue();
        }

        [Fact]
        public void Equals_GivenSameObject_ReturnsTrue()
        {
            sut.Equals(sut).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_WhenParamNameDiffers_ReturnsDifferentHashCode()
        {
            var otherException = new ConstructorCheckMissingException(otherParamName);
            sut.GetHashCode().Should().NotBe(otherException.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenSameParamName_ReturnsSameHashCode()
        {
            var otherException = new ConstructorCheckMissingException(paramName);
            sut.GetHashCode().Should().Be(otherException.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenCalledOnSameObject_ReturnsSameHashCode()
        {
            sut.GetHashCode().Should().Be(sut.GetHashCode());
        }
    }
}

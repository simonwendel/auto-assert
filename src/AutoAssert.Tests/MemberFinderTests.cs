namespace AutoAssert.Tests
{
    using System;
    using AutoAssert.Tests.TestTypes;
    using FluentAssertions;
    using Xunit;

    public partial class MemberFinderTests
    {
        private readonly Type type;
        private readonly MemberFinder sut;

        public MemberFinderTests()
        {
            type = typeof(ClassWithSomeMembers);
            sut = new MemberFinder();
        }

        [Fact]
        public void GetConstructors_GivenNullType_ThrowsException()
        {
            Action getting = () => sut.GetConstructors(null);
            getting.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("type");
        }

        [Fact]
        public void GetConstructors_GivenType_ReturnsConstructors()
        {
            sut.GetConstructors(type).Should().HaveCount(2);
        }

        [Fact]
        public void GetMethods_GivenNullType_ThrowsException()
        {
            Action getting = () => sut.GetMethods(null);
            getting.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("type");
        }

        [Fact]
        public void GetMethods_GivenType_ReturnsMethods()
        {
            sut.GetMethods(type).Should().HaveCount(2);
        }
    }
}

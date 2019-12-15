namespace AutoAssert.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class MemberFinderTests
    {
        private readonly Type type;
        private readonly MemberFinder sut;

        public MemberFinderTests()
        {
            type = typeof(Class);
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

#pragma warning disable IDE0060 // Remove unused parameter
        private class Class : ClassBase
        {
            public Class(float value)
                : base(value)
            {
            }

            public Class()
                : this(3.14f)
            {
            }

            public override void DoThings(string text)
                => throw new NotImplementedException();

            public int DoWork(int number, float value, string text)
                => 3;
        }

        private abstract class ClassBase
        {
            protected ClassBase(float value)
            {
            }

            public abstract void DoThings(string text);

            public void DoStuff(int number)
            {
            }
        }
#pragma warning restore IDE0060 // Remove unused parameter
    }
}

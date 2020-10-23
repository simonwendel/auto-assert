namespace AutoAssert.Tests.TestTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "Type used for testing reflection stuff, not doing anything with params really.")]
    public class ClassWithSomeMembers : ClassWithSomeMembersBase
    {
        public ClassWithSomeMembers(float value)
            : base(value)
        {
        }

        public ClassWithSomeMembers()
            : this(3.14f)
        {
        }

#pragma warning disable 67
        public event EventHandler BasicEvent;

        public event EventHandler<object> CompoundEvent;
#pragma warning restore 67

        public int OneProperty { get; set; }

        public override void DoThings(string text)
            => throw new NotImplementedException();

        public int DoWork(int number, float value, string text)
            => 3;
    }
}

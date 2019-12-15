namespace AutoAssert.Tests.TestTypes
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "Type used for testing reflection stuff, not doing anything with params really.")]
    public abstract class ClassWithSomeMembersBase
    {
        protected ClassWithSomeMembersBase(float value)
        {
        }

        public abstract void DoThings(string text);

        public void DoStuff(int number)
        {
        }
    }
}

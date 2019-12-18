namespace AutoAssert.Tests.TestTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// This class will fail to check for null, twice in methods, once in a property
    /// and once in the ctor.
    /// </summary>
    [SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "We don't use them here, but can't remove them either...")]
    public class ClassThatDoesntCheckAllForNull
    {
        public ClassThatDoesntCheckAllForNull(
            Dependencies.ISomeDependency someDependency,
            Dependencies.SomeAbstractDependency abstractDependency)
        {
            _ = someDependency ?? throw new ArgumentNullException();
        }

        public string PropertyWithOutChecks
        {
            set
            {
            }
        }

        public void UnitOfWork1(Dependencies.SomeValueObject param1, bool param2, object param3)
        {
            _ = param3 ?? throw new ArgumentNullException();
        }

        public void UnitOfWork2(string param1, object param3)
        {
            _ = param1 ?? throw new ArgumentNullException();
        }

        public void UnitOfWork3(bool someFlag)
        {
            // this should NOT be a violation. we only care about ArgumentNullException
            throw new NullReferenceException();
        }
    }
}

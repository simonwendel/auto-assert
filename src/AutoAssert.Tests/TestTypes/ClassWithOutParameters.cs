namespace AutoAssert.Tests.TestTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using static AutoAssert.Tests.TestTypes.Dependencies;

    /// <summary>
    /// This class will check for null in ctor and methods, for all but out
    /// variables, since those can't be checked until after assignment.
    /// </summary>
    [SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "We don't use them here, but can't remove them either...")]
    public class ClassWithOutParameters
    {
        public ClassWithOutParameters(out object param, object other)
        {
            _ = other ?? throw new ArgumentNullException();
            throw new NotImplementedException();
        }

        public void UnitOfWorkWithRefObject(out object param, object other)
        {
            _ = other ?? throw new ArgumentNullException();
            throw new NotImplementedException();
        }

        public void UnitOfWorkWithRefDependency(
            out ISomeDependency param,
            ISomeDependency other)
        {
            _ = other ?? throw new ArgumentNullException();
            throw new NotImplementedException();
        }

        public void UnitOfWorkWithAbstractRefDependency(
            out SomeAbstractDependency param,
            SomeAbstractDependency other)
        {
            _ = other ?? throw new ArgumentNullException();
            throw new NotImplementedException();
        }

        public void UnitOfWorkWithRefValueObject(
            out SomeValueObject param,
            SomeValueObject other)
        {
            _ = other ?? throw new ArgumentNullException();
            throw new NotImplementedException();
        }
    }
}

namespace AutoAssert.Tests.TestTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using static AutoAssert.Tests.TestTypes.Dependencies;

    /// <summary>
    /// This class will check for null in ctor and methods, all taking ref.
    /// </summary>
    [SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "We don't use them here, but can't remove them either...")]
    public class ClassWithRefParameters
    {
        public ClassWithRefParameters(ref object param)
        {
            Guard.AgainstNull(param, nameof(param));
            throw new NotImplementedException();
        }

        public void UnitOfWorkWithRefObject(ref object param)
        {
            Guard.AgainstNull(param, nameof(param));
            throw new NotImplementedException();
        }

        public void UnitOfWorkWithRefDependency(ref ISomeDependency param)
        {
            Guard.AgainstNull(param, nameof(param));
            throw new NotImplementedException();
        }

        public void UnitOfWorkWithAbstractRefDependency(ref SomeAbstractDependency param)
        {
            Guard.AgainstNull(param, nameof(param));
            throw new NotImplementedException();
        }

        public void UnitOfWorkWithRefValueObject(ref SomeValueObject param)
        {
            Guard.AgainstNull(param, nameof(param));
            throw new NotImplementedException();
        }
    }
}

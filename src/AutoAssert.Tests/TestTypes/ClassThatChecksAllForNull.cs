namespace AutoAssert.Tests.TestTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// This class will fail check all parameters for null, methods, properties and ctor.
    /// </summary>
    [SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "We don't use them here, but can't remove them either...")]
    public class ClassThatChecksAllForNull
    {
        public ClassThatChecksAllForNull(
            Dependencies.ISomeDependency someDependency,
            Dependencies.SomeAbstractDependency abstractDependency)
        {
            _ = someDependency ?? throw new ArgumentNullException();
            _ = abstractDependency ?? throw new ArgumentNullException();
        }

        public int SomeProperty { get; set; }

        public string PropertyWithChecks
        {
            set
            {
                _ = value ?? throw new ArgumentNullException();
            }
        }

        public void UnitOfWork1(string param1, Dependencies param3, bool param2)
        {
            _ = param1 ?? throw new ArgumentNullException();
            _ = param3 ?? throw new ArgumentNullException();

            // this should NOT be a violation. we only care about ArgumentNullException
            throw new InvalidOperationException();
        }

        public void UnitOfWork2(string param1, bool param2, object param3)
        {
            _ = param1 ?? throw new ArgumentNullException();
            _ = param3 ?? throw new ArgumentNullException();
        }

        public void UnitOfWork3(bool someFlag)
        {
            // this should NOT be a violation. we only care about ArgumentNullException
            throw new NotImplementedException();
        }

        internal void InternalUnitOfWork(object obj)
        {
        }

        private void PrivateUnitOfWork(object obj)
        {
        }
    }
}

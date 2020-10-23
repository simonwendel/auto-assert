namespace AutoAssert.Tests.TestTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "Type used for testing reflection stuff, not doing anything with params really.")]
    public class GenericClass<T>
    {
        public T UnitOfWork1(object obj, int i)
        {
            _ = obj ?? throw new ArgumentNullException();
            throw new NotImplementedException();
        }

        public T UnitOfWork1(float f, T obj)
        {
            _ = obj ?? throw new ArgumentNullException();
            throw new NotImplementedException();
        }

        public void UnitOfWork2(object obj)
        {
            _ = obj ?? throw new ArgumentNullException();
            throw new NotImplementedException();
        }
    }
}

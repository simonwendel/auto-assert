namespace AutoAssert.Tests.TestTypes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ClassWithYieldReturn
    {
        public IEnumerable UnitOfWorkWithYieldReturn(object obj)
        {
            _ = obj ?? throw new ArgumentNullException();
            for (int i = 0; i < 0; i++)
            {
                yield return new object();
            }
        }

        public IEnumerable<object> UnitOfWorkWithGenericYieldReturn(object obj)
        {
            _ = obj ?? throw new ArgumentNullException();
            for (int i = 0; i < 0; i++)
            {
                yield return new object();
            }
        }
    }
}

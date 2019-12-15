namespace AutoAssert
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal interface IMemberFinder
    {
        IEnumerable<ConstructorInfo> GetConstructors(Type type);

        IEnumerable<MethodInfo> GetMethods(Type type);
    }
}

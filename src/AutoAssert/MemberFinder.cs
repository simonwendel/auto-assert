namespace AutoAssert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class MemberFinder : IMemberFinder
    {
        public IEnumerable<ConstructorInfo> GetConstructors(Type type)
        {
            Guard.AgainstNull(type, nameof(type));
            return type.GetConstructors().Where(c => c.DeclaringType == type);
        }

        public IEnumerable<MethodInfo> GetMethods(Type type)
        {
            Guard.AgainstNull(type, nameof(type));
            return type.GetMethods().Where(c => c.DeclaringType == type);
        }
    }
}

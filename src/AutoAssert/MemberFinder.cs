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

            var eventMethods = GetEventMethods(type);
            return type.GetMethods().Where(
                c => c.DeclaringType == type
                && !eventMethods.Contains(c));
        }

        public IEnumerable<EventInfo> GetEvents(Type type)
        {
            Guard.AgainstNull(type, nameof(type));
            return type.GetEvents().Where(x => x.DeclaringType == type);
        }

        private IEnumerable<MethodInfo> GetEventMethods(Type type)
        {
            return GetEvents(type)
                .SelectMany(x => new[] { x.GetAddMethod(), x.GetRemoveMethod() });
        }
    }
}

namespace AutoAssert.Tests.TestTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    public class ReflectionValueFactory
    {
        /// <summary>
        /// Constructs a <see cref="MethodInfo"/> the old fashioned way, since they
        /// are super-hard to instantiate.
        /// </summary>
        /// <returns>A proper <see cref="MethodInfo"/> object.</returns>
        public static MethodInfo GetMethod()
            => new ReflectionValueFactory().GetType().GetMethod("DoStuff");

        [SuppressMessage(
            "Style",
            "IDE0060:Remove unused parameter",
            Justification = "We don't use them here, but can't remove them either...")]
        public object DoStuff(int integer, string text, double value, object obj)
        {
            throw new NotImplementedException();
        }
    }
}

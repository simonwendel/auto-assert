namespace AutoAssert
{
    using System;
    using System.Diagnostics;

    internal static class Guard
    {
        [DebuggerHidden]
        public static void AgainstNull([ValidatedNotNull] object parameter, string parameterName)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
        internal sealed class ValidatedNotNullAttribute : Attribute
        {
        }
    }
}

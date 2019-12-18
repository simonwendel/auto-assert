namespace AutoAssert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    public class NullCheckAsserter
    {
        private readonly IParameterListBuilder paramBuilder;
        private readonly IMemberFinder memberFinder;
        private readonly IMethodInvoker methodInvoker;

        internal NullCheckAsserter(
            IParameterListBuilder paramBuilder,
            IMemberFinder memberFinder,
            IMethodInvoker methodInvoker)
        {
            Guard.AgainstNull(paramBuilder, nameof(paramBuilder));
            Guard.AgainstNull(memberFinder, nameof(memberFinder));
            Guard.AgainstNull(methodInvoker, nameof(methodInvoker));
            this.paramBuilder = paramBuilder;
            this.memberFinder = memberFinder;
            this.methodInvoker = methodInvoker;
        }

        public void AllCheckedIn<T>()
        {
            var (sut, constructors, methods) = CreateTarget<T>();
            var exceptions = new List<CheckMissingException>();

            foreach (var constructor in constructors)
            {
                exceptions.AddRange(
                    NullCheckParameters(sut, constructor).Select(p => new ConstructorCheckMissingException(p)));
            }

            foreach (var method in methods)
            {
                exceptions.AddRange(
                    NullCheckParameters(sut, method).Select(p => new MethodCheckMissingException(method.Name, p)));
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        internal IEnumerable<string> NullCheckParameters(object systemUnderTest, MethodBase method)
        {
            Guard.AgainstNull(systemUnderTest, nameof(systemUnderTest));
            Guard.AgainstNull(method, nameof(method));

            var failingChecks = new List<string>();
            var paramCombos = paramBuilder.GetNullCombinations(method.GetParameters());

            foreach (var (paramName, paramList) in paramCombos)
            {
                try
                {
                    methodInvoker.Invoke(method, systemUnderTest, paramList);
                }
                catch (TargetInvocationException ex)
                when (ex.InnerException is ArgumentNullException)
                {
                    continue;
                }
                catch
                {
                }

                // if we get here, no null exception thrown
                failingChecks.Add(paramName);
            }

            return failingChecks;
        }

        private (T, IEnumerable<ConstructorInfo>, IEnumerable<MethodInfo>) CreateTarget<T>()
        {
            var typeUnderScrutiny = typeof(T);
            var sut = (T)FormatterServices.GetUninitializedObject(typeUnderScrutiny);
            var constructors = memberFinder.GetConstructors(typeUnderScrutiny);
            var methods = memberFinder.GetMethods(typeUnderScrutiny);
            return (sut, constructors, methods);
        }
    }
}

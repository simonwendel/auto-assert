namespace AutoAssert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoFixture.Kernel;

    internal class ParameterListBuilder : IParameterListBuilder
    {
        private readonly ISpecimenContext context;

        public ParameterListBuilder(ISpecimenContext context)
        {
            Guard.AgainstNull(context, nameof(context));
            this.context = context;
        }

        public IEnumerable<(string, object[])> GetNullCombinations(ParameterInfo[] parameters)
        {
            Guard.AgainstNull(parameters, nameof(parameters));
            for (int index = 0; index < parameters.Length; ++index)
            {
                var nullParameter = parameters[index];
                if (nullParameter.ParameterType.IsValueType || nullParameter.IsOut)
                {
                    // can't null check value types or out variables
                    continue;
                }

                var combo = parameters
                    .Select((parameter, idx) => idx == index ? null : ResolveValue(parameter));

                yield return (nullParameter.Name, combo.ToArray());
            }
        }

        private object ResolveValue(ParameterInfo parameter)
        {
            var type = parameter.ParameterType;
            if (parameter.IsOut)
            {
                type = GetTypeForOutParameter(parameter);
            }

            return context.Resolve(type);
        }

        private Type GetTypeForOutParameter(ParameterInfo parameter)
        {
            var name = parameter.ParameterType.FullName;
            name = name.Remove(name.Length - 1);
            return GetTypeByName(name);
        }

        private Type GetTypeByName(string name)
            => AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => a.GetType(name))
                .Distinct()
                .Single(t => t != null);
    }
}

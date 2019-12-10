namespace AutoAssert
{
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
                if (nullParameter.ParameterType.IsValueType)
                {
                    // can't null check value types
                    continue;
                }

                var combo = parameters
                    .Select((type, idx) => idx == index ? null : context.Resolve(type.ParameterType)).ToArray();

                yield return (nullParameter.Name, combo);
            }
        }
    }
}

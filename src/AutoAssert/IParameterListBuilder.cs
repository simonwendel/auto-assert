namespace AutoAssert
{
    using System.Collections.Generic;
    using System.Reflection;

    internal interface IParameterListBuilder
    {
        IEnumerable<(string, object[])> GetNullCombinations(ParameterInfo[] parameters);
    }
}

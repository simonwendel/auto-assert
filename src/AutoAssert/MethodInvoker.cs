namespace AutoAssert
{
    using System.Collections;
    using System.Linq;
    using System.Reflection;

    internal class MethodInvoker : IMethodInvoker
    {
        public void Invoke(MethodBase method, object systemUnderTest, object[] parameterList)
        {
            var result = method.Invoke(systemUnderTest, parameterList);
            if (result is IEnumerable enumerable)
            {
                _ = enumerable.OfType<object>().ToList();
            }
        }
    }
}

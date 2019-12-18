namespace AutoAssert
{
    using System.Reflection;

    internal class MethodInvoker : IMethodInvoker
    {
        public void Invoke(MethodBase method, object systemUnderTest, object[] parameterList)
            => _ = method.Invoke(systemUnderTest, parameterList);
    }
}

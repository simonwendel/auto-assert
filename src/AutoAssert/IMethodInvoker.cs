namespace AutoAssert
{
    using System.Reflection;

    internal interface IMethodInvoker
    {
        void Invoke(MethodBase method, object systemUnderTest, object[] parameterList);
    }
}

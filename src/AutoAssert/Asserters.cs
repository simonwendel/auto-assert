namespace AutoAssert
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Kernel;

    public static class Asserters
    {
        public static NullCheckAsserter GetNullCheckAsserter()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var context = new SpecimenContext(fixture);
            var paramBuilder = new ParameterListBuilder(context);
            var memberFinder = new MemberFinder();
            var methodInvoker = new MethodInvoker();
            return new NullCheckAsserter(paramBuilder, memberFinder, methodInvoker);
        }
    }
}

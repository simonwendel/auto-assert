namespace AutoAssert.Tests.TestTypes
{
    using System;

    public class ClassWithEvents
    {
#pragma warning disable 67
        public event EventHandler BasicEvent;

        public event EventHandler<object> CompoundEvent;
#pragma warning restore 67
    }
}

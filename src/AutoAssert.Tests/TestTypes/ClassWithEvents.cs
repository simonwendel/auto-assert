namespace AutoAssert.Tests.TestTypes
{
    using System;

    public class ClassWithEvents
    {
        public event EventHandler BasicEvent;

        public event EventHandler<object> CompoundEvent;
    }
}

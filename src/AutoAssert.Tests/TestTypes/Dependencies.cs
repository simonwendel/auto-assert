namespace AutoAssert.Tests.TestTypes
{
    public class Dependencies
    {
        public interface ISomeDependency
        {
            string DoThings();
        }

        public abstract class SomeAbstractDependency
        {
            public abstract int DoStuff();
        }

        public class SomeValueObject
        {
            public string ValueThing { get; } = "a value object";
        }
    }
}

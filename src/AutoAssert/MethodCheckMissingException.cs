namespace AutoAssert
{
    using System.Collections.Generic;

    public class MethodCheckMissingException : CheckMissingException
    {
        public MethodCheckMissingException(string methodName, string paramName)
        {
            MethodName = methodName;
            ParamName = paramName;
        }

        public string MethodName { get; }

        public string ParamName { get; }

        public override bool Equals(object obj)
            => obj is MethodCheckMissingException other
                && MethodName == other.MethodName
                && ParamName == other.ParamName;

        public override int GetHashCode()
        {
            var hashCode = 1288608676;
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(MethodName);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(ParamName);
            return hashCode;
        }
    }
}

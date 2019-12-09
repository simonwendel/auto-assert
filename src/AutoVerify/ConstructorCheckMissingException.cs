namespace AutoVerify
{
    using System.Collections.Generic;

    public class ConstructorCheckMissingException : CheckMissingException
    {
        public ConstructorCheckMissingException(string paramName)
        {
            ParamName = paramName;
        }

        public string ParamName { get; }

        public override bool Equals(object obj)
            => obj is ConstructorCheckMissingException other
                && ParamName.Equals(other.ParamName);

        public override int GetHashCode()
            => -1490103647 + EqualityComparer<string>.Default.GetHashCode(ParamName);
    }
}

using System;

namespace Trackwane.Framework.Common.Exceptions
{
    [Serializable]
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException()
        {
        }

        public BusinessRuleException(string message) : base(message)
        {
        }
    }
}

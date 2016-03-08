using System;

namespace Trackwane.Framework.Common.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string msg) : base(msg)
        {
        }
    }
}

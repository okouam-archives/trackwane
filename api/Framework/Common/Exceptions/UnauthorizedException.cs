using System;

namespace Trackwane.Framework.Common.Exceptions
{
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string msg) : base(msg) 
        {
        }

        public UnauthorizedException(string requesterId, string organizationKey) : base(CreateMessage(requesterId, organizationKey))
        {
        }

        private static string CreateMessage(string requesterId, string organizationKey)
        {
            return $"The user with ID <{requesterId}> cannot create resources in the organization with ID <{organizationKey}>";
        }
    }
}

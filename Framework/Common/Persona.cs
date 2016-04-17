using System;
using System.Collections.Generic;
using System.Linq;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Fixtures
{
    public class Persona
    {
        /* Public */

        public static UserClaims SystemManager(string applicationKey)
        {
            return new UserClaims { UserId = Guid.NewGuid().ToString(), IsSystemManager = true, ApplicationKey = applicationKey};
        }

        public static UserClaims User(string applicationKey, List<string> administrate = null, List<string> view = null, List<string> manage = null)
        {
            return new UserClaims { ApplicationKey = applicationKey, UserId = Guid.NewGuid().ToString(), Administrate = administrate, View = view, Manage = manage };
        }

        public static UserClaims Viewer(string applicationKey, params string[] organizations)
        {
            return new UserClaims
            {
                ApplicationKey = applicationKey,
                UserId = Guid.NewGuid().ToString(),
                View = organizations != null ? organizations.ToList() : new List<string>()
            };
        }

        public static UserClaims Administrator(string applicationKey, params string[] organizations)
        {
            return new UserClaims
            {
                ApplicationKey = applicationKey,
                UserId = Guid.NewGuid().ToString(),
                Administrate = organizations != null ? organizations.ToList() : new List<string>()
            };
        }

        public static UserClaims Manager(string applicationKey, params string[] organizations)
        {
            return new UserClaims
            {
                ApplicationKey = applicationKey,
                UserId = Guid.NewGuid().ToString(),
                Manage = organizations != null ? organizations.ToList() : new List<string>()
            };
        }
    }
}

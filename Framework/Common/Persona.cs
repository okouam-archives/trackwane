using System;
using System.Collections.Generic;
using System.Linq;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Fixtures
{
    public class Persona
    {
        /* Public */

        public static UserClaims SystemManager()
        {
            return new UserClaims { UserId = Guid.NewGuid().ToString(), IsSystemManager = true};
        }

        public static UserClaims User(List<string> administrate = null, List<string> view = null, List<string> manage = null)
        {
            return new UserClaims { UserId = Guid.NewGuid().ToString(), Administrate = administrate, View = view, Manage = manage };
        }

        public static UserClaims Viewer(params string[] organizations)
        {
            return new UserClaims
            {
                UserId = Guid.NewGuid().ToString(),
                View = organizations != null ? organizations.ToList() : new List<string>()
            };
        }

        public static UserClaims Administrator(params string[] organizations)
        {
            return new UserClaims
            {
                UserId = Guid.NewGuid().ToString(),
                Administrate = organizations != null ? organizations.ToList() : new List<string>()
            };
        }

        public static UserClaims Manager(params string[] organizations)
        {
            return new UserClaims
            {
                UserId = Guid.NewGuid().ToString(),
                Manage = organizations != null ? organizations.ToList() : new List<string>()
            };
        }
    }
}

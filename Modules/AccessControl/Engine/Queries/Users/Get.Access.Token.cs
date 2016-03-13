using System.Configuration;
using System.Linq;
using Raven.Client;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class GetAccessToken : Query<string>, IUnscopedQuery
    {
        private readonly IPlatformConfig platformConfig;

        public GetAccessToken(IDocumentStore documentStore, IPlatformConfig platformConfig) : base(documentStore)
        {
            this.platformConfig = platformConfig;
        }

        public string Execute(string email, string password)
        {
            return Execute(repository =>
            {
                var user = repository.Query<User>().Customize(x => x.WaitForNonStaleResults()).SingleOrDefault(x => x.Email == email);

                if (user != null && user.Credentials.IsValid(password))
                {
                    var userDetails = new FindByKey(documentStore).Execute(user.Key);

                    var userClaims = new UserClaims
                    {
                        UserId = userDetails.Key,
                        ParentOrganizationKey = userDetails.ParentOrganizationKey,
                        IsSystemManager = user.Role == Role.SystemManager,
                        Administrate = userDetails.Administrate.Select(x => x.Item1).ToList(),
                        Manage = userDetails.Manage.Select(x => x.Item1).ToList(),
                        View = userDetails.View.Select(x => x.Item1).ToList()
                    };

                    return userClaims.GenerateToken(platformConfig.SecretKey);
                }

                return null;
            });
        }
    }
}

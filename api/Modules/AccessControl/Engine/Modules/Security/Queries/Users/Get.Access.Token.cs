using System.Linq;
using Marten;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class GetAccessToken : Query<string>, IApplicationQuery
    {
        private readonly IPlatformConfig config;

        public GetAccessToken(IDocumentStore documentStore, IPlatformConfig config) : base(documentStore)
        {
            this.config = config;
        }

        public string Execute(string email, string password)
        {
            return Execute(repository =>
            {
                var user = repository.Query<User>().SingleOrDefault(x => x.Email == email && x.ApplicationKey == ApplicationKey);

                if (user != null && user.Credentials.IsValid(password))
                {
                    var query = new FindByKey(documentStore) {ApplicationKey = ApplicationKey};
                    var userDetails = query.Execute(user.Key);

                    var userClaims = new UserClaims
                    {
                        UserId = userDetails.Key,
                        ParentOrganizationKey = userDetails.ParentOrganizationKey,
                        IsSystemManager = user.Role == Role.SystemManager,
                        Administrate = userDetails.Administrate.Select(x => x.Key).ToList(),
                        Manage = userDetails.Manage.Select(x => x.Key).ToList(),
                        View = userDetails.View.Select(x => x.Key).ToList()
                    };

                    return userClaims.GenerateToken(config.SecretKey);
                }

                return null;
            });
        }
    }
}

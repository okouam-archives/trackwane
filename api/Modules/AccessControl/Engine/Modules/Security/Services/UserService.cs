using System.Linq;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Services
{
    public class UserService : IUserService
    {
        public bool IsExistingUser(string applicationKey, string userId, IRepository repository)
        {
            return !string.IsNullOrEmpty(userId) && repository.Query<User>().Any(x => x.ApplicationKey == applicationKey && x.Key == userId);
        }

        public bool IsExistingEmail(string applicationKey, string email, IRepository repository)
        {
            return repository.Query<User>().Any(x => x.Email == email && x.ApplicationKey == applicationKey);
        }
    }
}

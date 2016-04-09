using System.Linq;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Services
{
    public class UserService : IUserService
    {
        public bool IsExistingUser(string userId, IRepository repository)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var existingUser = repository.Load<User>(userId);

                return existingUser != null;
            }

            return false;
        }

        public bool IsExistingEmail(string email, IRepository repository)
        {
            return repository.Query<User>().Any(x => x.Email == email);
        }
    }
}

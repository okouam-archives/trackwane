using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Services
{
    public interface IUserService
    {
        bool IsExistingUser(string userId, IRepository repository);

        bool IsExistingEmail(string email, IRepository repository);
    }
}
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Services
{
    public interface IUserService
    {
        bool IsExistingUser(string applicationKey, string userId, IRepository repository);

        bool IsExistingEmail(string applicationKey, string email, IRepository repository);
    }
}
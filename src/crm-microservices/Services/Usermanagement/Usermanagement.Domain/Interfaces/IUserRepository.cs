using Usermanagement.Domain.Entities.UserDetails;

namespace Usermanagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<User> GetUserByUsername(string username);
    }
}

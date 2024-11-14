using Microsoft.EntityFrameworkCore;
using Usermanagement.Domain.Entities.UserDetails;
using Usermanagement.Domain.Interfaces;
using Usermanagement.Infrastructure.Data;

namespace Usermanagement.Infrastructure.Persistence.UserDetails
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUser(User user)
        {
            if (user == null)
            {
                return 0;
            }
            await _context.Users.AddAsync(user);
            var response = await _context.SaveChangesAsync();
            return response;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new InvalidOperationException($"Username is null.");
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            return user ?? new User();
        }
    }
}

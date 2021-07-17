namespace SocialMedia.INFRASTRUCTURE.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserRepository: IUserRepository
    {
        private readonly SocialMediaContext _context;

        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }

        // READ
        // ----
        public async Task<IEnumerable<User>> GetUsers()
        {
            var post = await _context.Users.ToListAsync();
            return post;
        }

        public async Task<User> GetUsers(int id)
        {
            var post = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            return post;
        }
    }
}

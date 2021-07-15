namespace SocialMedia.INFRASTRUCTURE.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;

        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publicacion>> GetPosts()
        {
            var post = await _context.Publicacion.ToListAsync();
            return post;
        }
    }
}

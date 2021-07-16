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

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var post = await _context.Posts.ToListAsync();
            return post;
        }

        public async Task<Post> GetPosts(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

    }
}

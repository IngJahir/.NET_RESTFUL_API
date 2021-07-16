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

        // CREATE
        // ------
        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        // READ
        // ----
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

        // UPDATE
        // ------
        public async Task<bool> UpdatePost(Post post) 
        {
            // Traer datos segun id
            // --------------------
            var currentPost = await GetPosts(post.PostId);
            currentPost.Date = post.Date;
            currentPost.Description = post.Description;
            currentPost.Image = post.Image;

            // Guardar cambios
            // ---------------
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        // DELETE
        // ------
        public async Task<bool> DeletePost(int id)
        {
            var currentPost = await GetPosts(id);
            _context.Posts.Remove(currentPost);

            // Guardar cambios
            // ---------------
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

    }
}

namespace SocialMedia.CORE.Interfaces
{
    using SocialMedia.CORE.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPosts(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}

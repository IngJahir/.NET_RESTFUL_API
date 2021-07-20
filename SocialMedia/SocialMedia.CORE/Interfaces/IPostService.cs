namespace SocialMedia.CORE.Interfaces
{
    using SocialMedia.CORE.CustomEntities;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.QueryFilters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostService
    {
        PagedList<Post> GetPosts(PostQueryFilter filters);
        Task<Post> GetPosts(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}

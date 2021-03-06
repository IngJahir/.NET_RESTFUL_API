namespace SocialMedia.CORE.Interfaces
{
    using SocialMedia.CORE.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPostRepository: IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUser(int userId);
    }
}

namespace SocialMedia.CORE.Interfaces
{
    using SocialMedia.CORE.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Post> PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> commentRepository { get; }

        void SaveChanges();
        Task SaveChanesAsync();
    }
}

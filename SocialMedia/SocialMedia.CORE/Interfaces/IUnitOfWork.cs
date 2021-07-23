namespace SocialMedia.CORE.Interfaces
{
    using SocialMedia.CORE.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> commentRepository { get; }
        ISecurityRepository SecurityRepository { get; }
        void SaveChanges();
        Task SaveChanesAsync();
    }
}

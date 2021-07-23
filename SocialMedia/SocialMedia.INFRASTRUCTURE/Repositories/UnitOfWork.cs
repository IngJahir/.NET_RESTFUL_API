namespace SocialMedia.INFRASTRUCTURE.Repositories
{
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Data;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IPostRepository _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly ISecurityRepository _securityRepository;

        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context);

        public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        public IRepository<Comment> commentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);

        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChanesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

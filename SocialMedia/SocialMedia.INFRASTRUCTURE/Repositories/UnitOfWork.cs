namespace SocialMedia.INFRASTRUCTURE.Repositories
{
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Data;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;

        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        public IRepository<Post> PostRepository => _postRepository ?? new BaseRepository<Post>(_context);

        public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        public IRepository<Comment> commentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);

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

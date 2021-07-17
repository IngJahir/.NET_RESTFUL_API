namespace SocialMedia.INFRASTRUCTURE.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        // CREATE
        // ------
        public async Task Add(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        // READ
        // ----
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        // UPDATE
        // ------
        public async Task Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        // DELETE
        // ------
        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
            _context.SaveChanges();
        }

    }
}

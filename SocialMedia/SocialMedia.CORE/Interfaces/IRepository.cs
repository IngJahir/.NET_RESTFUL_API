namespace SocialMedia.CORE.Interfaces
{
    using SocialMedia.CORE.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : BaseEntity
    {
        Task Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Update(T entity);
        Task Delete(int id);
    }
}

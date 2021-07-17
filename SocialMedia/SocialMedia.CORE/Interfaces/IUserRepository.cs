namespace SocialMedia.CORE.Interfaces
{
    using SocialMedia.CORE.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUsers(int id);
    }
}

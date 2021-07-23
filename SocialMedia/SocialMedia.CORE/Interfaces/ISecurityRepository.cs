namespace SocialMedia.CORE.Interfaces
{
    using SocialMedia.CORE.Entities;
    using System.Threading.Tasks;

    public interface ISecurityRepository: IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}

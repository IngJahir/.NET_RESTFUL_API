namespace SocialMedia.CORE.Services
{
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using System.Threading.Tasks;

    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLoginByCredential(UserLogin userLogin)
        {
            return await _unitOfWork.SecurityRepository.GetLoginByCredentials(userLogin);
        }

        public async Task RegisterUser(Security security)
        {
            await _unitOfWork.SecurityRepository.Add(security);
            await _unitOfWork.SaveChanesAsync();
        }


    }
}

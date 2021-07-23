using SocialMedia.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.CORE.Interfaces
{
    public interface ISecurityService
    {

        Task<Security> GetLoginByCredential(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}

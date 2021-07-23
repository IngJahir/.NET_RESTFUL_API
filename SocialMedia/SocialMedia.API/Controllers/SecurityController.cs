namespace SocialMedia.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SocialMedia.API.Responses;
    using SocialMedia.CORE.DTOs;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Enumerations;
    using SocialMedia.CORE.Interfaces;
    using System.Threading.Tasks;

    [Authorize(Roles =nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public SecurityController(ISecurityService securityService, IMapper mapper)
        {
            _securityService = securityService;
            _mapper = mapper;
        }

        // CREATE 
        // ------
        [HttpPost]
        public async Task<IActionResult> Post(SecurityDto securityDto)
        {
            var security = _mapper.Map<Security>(securityDto);
            await _securityService.RegisterUser(security);

            securityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);

            return Ok(response);
        }
    }
}

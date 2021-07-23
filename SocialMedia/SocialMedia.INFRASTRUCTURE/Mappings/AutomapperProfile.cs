namespace SocialMedia.INFRASTRUCTURE.Mappings
{
    using AutoMapper;
    using SocialMedia.CORE.DTOs;
    using SocialMedia.CORE.Entities;

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();

            CreateMap<Security, SecurityDto>().ReverseMap();
        }
    }
}

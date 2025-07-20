using AutoMapper;
using PlatformService.Data;
using PlatformService.Repository.DTOs;

namespace PlatformService.Repository.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();
            CreateMap<PlatformReadDTO, PlatformPublishedDTO>();
        }
    }
}

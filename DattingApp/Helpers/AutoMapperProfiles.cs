using AutoMapper;
using DattingAppApi.DTOs;
using DattingAppApi.Entities;
using DattingAppApi.Extensions;

namespace DattingAppApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMainPhoto)!.Url));

            CreateMap<Photo,PhotoDto>();
        }
    }
}

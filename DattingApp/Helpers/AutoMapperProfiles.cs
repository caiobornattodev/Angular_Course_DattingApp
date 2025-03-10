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

            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<string, DateOnly>().ConstructUsing(s => DateOnly.Parse(s));

            CreateMap<Message, MessageDto>()
                .ForMember(d => d.SenderPhotoUrl,
                    o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMainPhoto)!.Url))
                .ForMember(d => d.RecipeintPhotoUrl,
                    o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMainPhoto)!.Url));
        }
    }
}

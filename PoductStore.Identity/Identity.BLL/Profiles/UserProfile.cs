using AutoMapper;
using AutoMapper.Internal;
using PoductStore.Identity.Identity.BLL.Dtos;
using PoductStore.Identity.Identity.DAL.Models;

namespace PoductStore.Identity.Identity.BLL.Profiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, User>()
            .ForMember(dst => dst.Email,
                opt => opt
                    .MapFrom(src => src.Email))
            .ForMember(dst => dst.Password,
                opt => opt
                    .MapFrom(src => src.Password))
            .ForMember(dst => dst.CreatedAt,
                opt => opt
                    .MapFrom(src => DateTime.UtcNow))
            .ForMember(dst => dst.FullName,
                opt => opt
                    .MapFrom(src => src.FullName))
            .ForMember(dst => dst.UserName,
                opt => opt
                    .MapFrom(src => src.UserName));
        CreateMap<User, LoginUserDto>();


    }
}
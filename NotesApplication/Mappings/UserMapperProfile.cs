using AutoMapper;
using NotesApplication.DTOs.User;
using NotesApplication.Models;

namespace NotesApplication.Mappings;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.UserRole)));
        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.UserRole)));
        CreateMap<User, UserReadDto>()
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes.Select(n => n.Title)))
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserRole.ToString()));
    }
}
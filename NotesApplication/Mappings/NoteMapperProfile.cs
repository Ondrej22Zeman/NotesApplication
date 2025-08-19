namespace NotesApplication.Mappings;

using AutoMapper;
using NotesApplication.DTOs;
using NotesApplication.Models;

public class NoteMapperProfile : Profile
{
    public NoteMapperProfile()
    {
        CreateMap<Note, NoteReadDetailDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Title)))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));

        CreateMap<Note, NoteReadListDto>();

        CreateMap<NoteUpdateDto, Note>();

        CreateMap<NoteCreateDto, Note>();
    }
}
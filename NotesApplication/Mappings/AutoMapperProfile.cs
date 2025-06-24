namespace NotesApplication.Mappings;

using AutoMapper;
using NotesApplication.DTOs;
using NotesApplication.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Note, NoteDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));

        CreateMap<NoteDto, Note>()
            .ForMember(dest => dest.Tags, opt => opt.Ignore());
    }
}
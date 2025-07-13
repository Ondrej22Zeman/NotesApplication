using AutoMapper;
using NotesApplication.DTOs.Tag;
using NotesApplication.Models;

namespace NotesApplication.Mappings;

public class TagMapperProfile : Profile
{
    public TagMapperProfile()
    {
        CreateMap<TagCreateDto, Tag>();
        CreateMap<TagUpdateDto, Tag>();
        CreateMap<Tag, TagReadDto>()
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes.Select(n => n.Title)));
    }
}
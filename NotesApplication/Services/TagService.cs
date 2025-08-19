using AutoMapper;
using NotesApplication.DTOs.Tag;
using System.Linq;
using NotesApplication.Repositories;

namespace NotesApplication.Services;

using Data;
using Models;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public TagService(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TagReadDto>> GetAsync()
    {
        var tags = await _tagRepository.GetAllAsync();

        var tagDtos = tags.Select(t => _mapper.Map<Tag, TagReadDto>(t));
        
        return tagDtos;
    }

    public async Task<TagReadDto?> GetByIdAsync(Guid id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);
        
        if (tag is null) throw new Exception($"Tag could not be found, id = {id}");

        return _mapper.Map<Tag, TagReadDto>(tag);
    }

    public async Task<TagReadDto> CreateAsync(TagCreateDto tagDto)
    {
        var tag = _mapper.Map<TagCreateDto, Tag>(tagDto);
        tag = await _tagRepository.CreateAsync(tag);

        await _tagRepository.SaveChangesAsync();

        return _mapper.Map<Tag, TagReadDto>(tag);
    }

    public async Task<TagReadDto> UpdateAsync(TagUpdateDto tagDto, Guid id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag == null)
        {
            throw new KeyNotFoundException($"Tag could not be found, id = {id}");
        }

        _mapper.Map(tagDto, tag);

        var updatedTag = _tagRepository.Update(tag);
        await _tagRepository.SaveChangesAsync();

        return _mapper.Map<TagReadDto>(updatedTag);
    }

    public async Task DeleteAsync(Guid id)
    {
        var tagToDelete = await _tagRepository.GetByIdAsync(id);
        
        if (tagToDelete == null)
            throw new KeyNotFoundException($"Tag could not be found, id = {id}");
        
        _tagRepository.Delete(tagToDelete);

        await _tagRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<TagReadDto>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        ids = ids.ToList();
        var tags = (await _tagRepository.GetByIdsAsync(ids)).ToList();

        if (tags.Count() != ids.Count())
        {
            throw new KeyNotFoundException("Some tags not found");
        }

        var tagDtos = tags.Select(t => _mapper.Map<TagReadDto>(t));

        return tagDtos;
    }
}
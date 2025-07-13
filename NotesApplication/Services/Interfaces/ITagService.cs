using NotesApplication.DTOs.Tag;

namespace NotesApplication.Services;

using Models;

public interface ITagService
{
    Task<IEnumerable<TagReadDto>> GetAsync();
    Task<TagReadDto?> GetByIdAsync(Guid id);
    Task<TagReadDto> CreateAsync(TagCreateDto tagDto);
    Task<TagReadDto> UpdateAsync(TagUpdateDto tagDto, Guid id);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<TagReadDto>> GetByIdsAsync(IEnumerable<Guid> ids);
}
using NotesApplication.DTOs.Tag;

namespace NotesApplication.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagReadDto>>> GetTagsAsync()
    {
        var tagDtos =  await _tagService.GetAsync();

        return Ok(tagDtos);
    }

    [HttpGet("{id}", Name = "GetTag")]
    public async Task<ActionResult<TagReadDto?>> GetTag(Guid id)
    {
        try
        {
            var tagDto = await _tagService.GetByIdAsync(id);
            return Ok(tagDto);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<TagReadDto>> CreateTagAsync(TagCreateDto tagDto)
    {
        var createdTag = await _tagService.CreateAsync(tagDto);

        return CreatedAtRoute(nameof(GetTag), new { id = createdTag.Id }, createdTag);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Tag>> UpdateTag(TagUpdateDto tagDto, Guid id)
    {
        try
        {
            var updatedTag = await _tagService.UpdateAsync(tagDto, id);
            return Ok(updatedTag);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTag(Guid id)
    {
        try
        {
            await _tagService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("ByIds")]
    public async Task<ActionResult<IEnumerable<TagReadDto>>> GetTagsByIdsAsync(IEnumerable<Guid> ids)
    {
        try
        {
            var tagDtos = await _tagService.GetByIdsAsync(ids);
            return Ok(tagDtos);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
namespace NotesApplication.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Tag>> GetTags()
    {
        return _tagService.GetTags();
    }

    [HttpGet("{id}")]
    public ActionResult<Tag> GetTag(int id)
    {
        return _tagService.GetTagById(id);
    }

    [HttpPost("create")]
    public ActionResult<Tag> CreateTag(Tag tag)
    {
        var isCreated = _tagService.CreateTag(tag);

        if (!isCreated)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
    }

    [HttpPut("update")]
    public ActionResult<Tag> UpdateTag(Tag tag)
    {
        var isUpdated = _tagService.UpdateTag(tag);

        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok(tag);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteTag(int id)
    {
        var isDeleted = _tagService.DeleteTag(id);

        if (!isDeleted)
        {
            return BadRequest();
        }
        
        return NoContent();
    }
}
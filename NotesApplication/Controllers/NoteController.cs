using NotesApplication.DTOs;

namespace NotesApplication.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApplication.Services;


[ApiController]
[Route("[controller]")]
[Authorize]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IAsyncEnumerable<NoteReadListDto>>> GetNotesAsync()
    {
        var noteDtos = await _noteService.GetNotesAsync();
        
        return Ok(noteDtos);
    }

    [HttpGet("{id}", Name = "GetNote")]
    public async Task<ActionResult<NoteReadDetailDto>> GetNoteAsync(Guid id)
    {
        try
        {
            var noteDto = await _noteService.GetNoteByIdAsync(id);

            return noteDto;
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<NoteReadDetailDto>> CreateNoteAsync(NoteCreateDto noteCreateDto)
    {
        var createdNote = await _noteService.CreateNoteAsync(noteCreateDto);

        return CreatedAtRoute(nameof(GetNoteAsync), new { id = createdNote.Id }, createdNote);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NoteReadDetailDto>> UpdateNote(NoteUpdateDto noteUpdate, Guid id)
    {
        try
        {
            var updatedNote = await _noteService.UpdateNoteAsync(noteUpdate, id);
            return Ok(updatedNote);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNote(Guid id)
    {
        try
        {
            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("{noteId}/tags/{tagId}")]
    public async Task<ActionResult<NoteReadDetailDto>> AddTagToNoteAsync(Guid noteId, Guid tagId)
    {
        try
        {
            var noteDto = await _noteService.AddTagToNoteAsync(noteId, tagId);
            return Ok(noteDto);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpDelete("{noteId}/tags/{tagId}")]
    public async Task<ActionResult> RemoveTagToNoteAsync(Guid noteId, Guid tagId)
    {
        try
        {
            await _noteService.RemoveTagFromNoteAsync(noteId, tagId);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}
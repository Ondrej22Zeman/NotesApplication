using NotesApplication.DTOs;

namespace NotesApplication.Controllers;

using Microsoft.AspNetCore.Mvc;
using NotesApplication.Services;


[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<NoteReadDto>>> GetNotesAsync()
    {
        var noteDtos = await _noteService.GetNotesAsync();
        
        return Ok(noteDtos);
    }

    [HttpGet("{id}", Name = "GetNote")]
    public async Task<ActionResult<NoteReadDto>> GetNoteByIdAsync(Guid id)
    {
        var noteDto = await _noteService.GetNoteByIdAsync(id);

        if (noteDto == null)
        {
            return NotFound();
        }
        
        return noteDto;
    }

    [HttpPost]
    public async Task<ActionResult<NoteReadDto>> CreateNoteAsync(NoteCreateDto noteCreateDto)
    {
        var createdNote = await _noteService.CreateNoteAsync(noteCreateDto);

        return CreatedAtRoute("GetNote", new { id = createdNote.Id }, createdNote);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NoteReadDto>> UpdateNote(NoteUpdateDto noteUpdate, Guid id)
    {
        try
        {
            var updatedNote = await _noteService.UpdateNoteAsync(noteUpdate, id);
            return Ok(updatedNote);
        }
        catch (Exception e)
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
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("{noteId}/tags/{tagId}")]
    public async Task<ActionResult<NoteReadDto>> AddTagToNoteAsync(Guid noteId, Guid tagId)
    {
        try
        {
            var noteDto = await _noteService.AddTagToNoteAsync(noteId, tagId);
            return Ok(noteDto);
        }
        catch (Exception e)
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
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
}
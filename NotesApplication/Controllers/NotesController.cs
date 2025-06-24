using AutoMapper;
using NotesApplication.DTOs;
using NotesApplication.Mappings;

namespace NotesApplication.Controllers;

using Microsoft.AspNetCore.Mvc;
using NotesApplication.Models;
using NotesApplication.Services;


[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly IMapper _mapper;

    public NotesController(INoteService noteService, IMapper mapper)
    {
        _noteService = noteService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Note>> GetNotes()
    {
        var notes = _noteService.GetNotes();
        
        var dtoNotes = notes.Select(n => _mapper.Map<NoteDto>(n));
        return Ok(dtoNotes);
    }

    [HttpGet("{id}")]
    public ActionResult<Note> GetNote(int id)
    {
        var note = _noteService.GetNoteById(id);
        return Ok(_mapper.Map<NoteDto>(note));
    }

    [HttpPost("create")]
    public ActionResult<Note> CreateNote(Note note)
    {
        var isCreated = _noteService.CreateNote(note);

        if (!isCreated)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }

    [HttpPut("update")]
    public ActionResult<Note> UpdateNote(Note note)
    {
        var isUpdated = _noteService.UpdateNote(note);

        if (!isUpdated)
        {
            return BadRequest();
        }
        
        return Ok(note);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteNote(int id)
    {
        var isDeleted = _noteService.DeleteNote(id);

        if (!isDeleted)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
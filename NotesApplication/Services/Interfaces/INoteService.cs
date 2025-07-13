using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotesApplication.DTOs;

namespace NotesApplication.Services;

using Models;

public interface INoteService
{
    Task<IEnumerable<NoteReadDto>> GetNotesAsync();
    Task<NoteReadDto?> GetNoteByIdAsync(Guid id);
    Task<NoteReadDto> CreateNoteAsync(NoteCreateDto noteCreateDto);
    Task<NoteReadDto> UpdateNoteAsync(NoteUpdateDto noteUpdateDto, Guid id);
    Task DeleteNoteAsync(Guid id);
    Task<NoteReadDto> AddTagToNoteAsync(Guid noteId, Guid tagId);
    Task RemoveTagFromNoteAsync(Guid noteId, Guid tagId);
}
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotesApplication.DTOs;

namespace NotesApplication.Services;

using Models;

public interface INoteService
{
    Task<IEnumerable<NoteReadListDto>> GetNotesAsync();
    Task<NoteReadDetailDto> GetNoteByIdAsync(Guid id);
    Task<NoteReadDetailDto> CreateNoteAsync(NoteCreateDto noteCreateDto);
    Task<NoteReadDetailDto> UpdateNoteAsync(NoteUpdateDto noteUpdateDto, Guid id);
    Task DeleteNoteAsync(Guid id);
    Task<NoteReadDetailDto> AddTagToNoteAsync(Guid noteId, Guid tagId);
    Task RemoveTagFromNoteAsync(Guid noteId, Guid tagId);
}
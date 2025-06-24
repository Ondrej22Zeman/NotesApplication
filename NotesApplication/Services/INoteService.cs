using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NotesApplication.Services;

using Models;

public interface INoteService
{
    List<Note> GetNotes();
    Note GetNoteById(int id);
    bool CreateNote(Note note);
    bool UpdateNote(Note note);
    bool DeleteNote(int id);
    bool AddTag(int noteId, int tagId);
    bool RemoveTag(int noteId, int tagId);
}
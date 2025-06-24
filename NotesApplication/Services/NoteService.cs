namespace NotesApplication.Services;

using Data;
using Models;

public class NoteService : INoteService
{
    private readonly NotesDbContext _context;

    public NoteService(NotesDbContext context)
    {
        _context = context;
    }

    public List<Note> GetNotes()
    {
        var notes = _context.Notes.ToList();
        return notes;
    }

    public Note GetNoteById(int id)
    {
        Note note = _context.Notes.FirstOrDefault(n => n.Id == id);
        return note;
    }

    public bool CreateNote(Note note)
    {
        _context.Notes.Add(note);
        var changes = _context.SaveChanges();

        return changes > 0;
    }

    public bool UpdateNote(Note note)
    {
        _context.Notes.Update(note);
        var changes = _context.SaveChanges();

        return changes > 0;
    }

    public bool DeleteNote(int id)
    {
        var note = _context.Notes.FirstOrDefault(n => n.Id == id);

        if (note != null)
        {
            _context.Notes.Remove(GetNoteById(id));
            _context.SaveChanges();
            return true;
        }
        
        return false;
    }

    public bool AddTag(int noteId, int tagId)
    {
        var note = _context.Notes.FirstOrDefault(n => n.Id == noteId);
        var tag = _context.Tags.FirstOrDefault(t => t.Id == tagId);
        if (tag == null || note == null)
        {
            return false;
        }
        note.Tags.Add(tag);
        return UpdateNote(note);
    }

    public bool RemoveTag(int noteId, int tagId)
    {
        var note = _context.Notes.FirstOrDefault(n => n.Id == noteId);
        var tag = _context.Tags.FirstOrDefault(t => t.Id == tagId);
        
        if (tag == null || note == null)
        {
            return false;
        }
        note.Tags.Remove(tag);
        return UpdateNote(note);
    }
}
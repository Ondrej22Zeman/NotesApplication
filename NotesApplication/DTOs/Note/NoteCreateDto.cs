namespace NotesApplication.DTOs;

using Models;

public class NoteCreateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public List<Guid> TagIds { get; set; }
}
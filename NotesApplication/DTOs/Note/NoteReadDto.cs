namespace NotesApplication.DTOs;

public class NoteReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
    public List<string> Tags { get; set; }
}
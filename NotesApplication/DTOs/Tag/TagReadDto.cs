namespace NotesApplication.DTOs.Tag;

public class TagReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<string> Notes { get; set; }
}
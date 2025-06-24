namespace NotesApplication.Models;

public class Note
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public Guid UserId { get; set; }

    public List<Tag> Tags { get; set; } = new();
}
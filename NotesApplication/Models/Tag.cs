namespace NotesApplication.Models;

public class Tag
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
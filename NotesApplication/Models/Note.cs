using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApplication.Models;

public class Note
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

}
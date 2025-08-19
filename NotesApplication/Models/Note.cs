using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApplication.Models;

public class Note
{
    [Required]
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; }
    
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

}
using System.ComponentModel.DataAnnotations;

namespace NotesApplication.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(50)]
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public UserRole UserRole { get; set; } = UserRole.User;
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
using NotesApplication.Models;

namespace NotesApplication.DTOs.User;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string UserRole { get; set; }
    public IEnumerable<string> Notes { get; set; }
}
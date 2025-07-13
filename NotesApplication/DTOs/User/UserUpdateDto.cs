using NotesApplication.Models;

namespace NotesApplication.DTOs.User;

public class UserUpdateDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string UserRole { get; set; }
}
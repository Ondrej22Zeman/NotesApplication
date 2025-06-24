namespace NotesApplication.DTOs;

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Notes { get; set; }
}
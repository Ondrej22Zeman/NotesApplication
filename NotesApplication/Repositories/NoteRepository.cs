using Microsoft.EntityFrameworkCore;
using NotesApplication.Data;
using NotesApplication.Models;

namespace NotesApplication.Repositories;

public class NoteRepository : Repository<Note>, INoteRepository
{
    public NoteRepository(AppDbContext context): base(context) {}

    public async Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId)
    {
        var notes = await GetAllAsync();

        return notes.Where(n => n.UserId == userId);
    }
}
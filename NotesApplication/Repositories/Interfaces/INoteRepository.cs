using NotesApplication.Models;

namespace NotesApplication.Repositories;

public interface INoteRepository : IRepository<Note>
{
    Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId);
}
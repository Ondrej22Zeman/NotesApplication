using NotesApplication.Models;

namespace NotesApplication.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<IEnumerable<Tag>> GetByIdsAsync(IEnumerable<Guid> ids);
}
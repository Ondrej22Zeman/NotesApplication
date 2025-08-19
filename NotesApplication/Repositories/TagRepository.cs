using Microsoft.EntityFrameworkCore;
using NotesApplication.Data;
using NotesApplication.Models;

namespace NotesApplication.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context){}

    public async Task<IEnumerable<Tag>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        var tags = (await GetAllAsync()).ToList();

        var tagsByIds = tags.Where(t => ids.Contains(t.Id));
            
        return tagsByIds;
    }
}
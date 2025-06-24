namespace NotesApplication.Services;

using Data;
using Models;

public class TagService : ITagService
{
    private readonly NotesDbContext _context;

    public TagService(NotesDbContext context)
    {
        _context = context;
    }

    public List<Tag> GetTags()
    {
        var tags = _context.Tags.ToList();
        return tags;
    }

    public Tag GetTagById(int id)
    {
        Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);
        return tag;
    }

    public bool CreateTag(Tag tag)
    {
        _context.Tags.Add(tag);
        var changes = _context.SaveChanges();

        return changes > 0;
    }

    public bool UpdateTag(Tag tag)
    {
        _context.Tags.Update(tag);
        var changes = _context.SaveChanges();

        return changes > 0;
    }

    public bool DeleteTag(int id)
    {
        var Tag = _context.Tags.FirstOrDefault(t => t.Id == id);
        if (Tag != null)
        {
            _context.Tags.Remove(Tag);
            _context.SaveChanges();

            return true;
        }

        return false;
    }
}
namespace NotesApplication.Services;

using Models;

public interface ITagService
{
    List<Tag> GetTags();
    Tag GetTagById(int id);
    bool CreateTag(Tag tag);
    bool UpdateTag(Tag tag);
    bool DeleteTag(int id);
}
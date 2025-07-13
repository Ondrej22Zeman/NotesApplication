namespace NotesApplication.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    T Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}
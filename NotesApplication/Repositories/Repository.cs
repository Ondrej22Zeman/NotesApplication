using Microsoft.EntityFrameworkCore;
using NotesApplication.Data;

namespace NotesApplication.Repositories;



public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
    
    public async Task<T> CreateAsync(T entity){
        var entry = await _dbSet.AddAsync(entity);
        return entry.Entity;
    }

    public T Update(T entity)
    { 
        var entry = _dbSet.Update(entity);
        return entry.Entity;
    }
    public void Delete (T entity) => _dbSet.Remove(entity);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

}
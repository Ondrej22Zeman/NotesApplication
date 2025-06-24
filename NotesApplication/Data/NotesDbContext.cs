namespace NotesApplication.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Note> Notes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Note>()
            .HasMany(n => n.Tags)
            .WithMany(m => m.Notes);
    }
    
}
namespace NotesApplication.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Note> Notes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Note>()
            .HasMany(n => n.Tags)
            .WithMany(m => m.Notes);
        
        modelBuilder.Entity<User>()
            .HasMany(n => n.Notes)
            .WithOne(u => u.User);
    }
}
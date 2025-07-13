using NotesApplication.Data;

namespace NotesApplication.Repositories;

using Models;

public class UserRepository : Repository<User>,IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) {}
    
}
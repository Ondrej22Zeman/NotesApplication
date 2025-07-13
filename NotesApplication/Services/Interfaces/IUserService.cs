using NotesApplication.DTOs.User;
using NotesApplication.Models;

namespace NotesApplication.Services;

public interface IUserService
{
    Task<IEnumerable<UserReadDto>> GetAsync();
    Task<UserReadDto?> GetByIdAsync(Guid id);
    Task<UserReadDto> CreateAsync(UserCreateDto user);
    Task<UserReadDto> UpdateAsync(Guid id, UserUpdateDto user);
    Task DeleteAsync(Guid id);
}
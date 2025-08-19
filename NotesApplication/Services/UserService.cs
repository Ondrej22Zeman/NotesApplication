using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NotesApplication.DTOs.User;
using NotesApplication.Models;
using NotesApplication.Repositories;

namespace NotesApplication.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    
    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<IEnumerable<UserReadDto>> GetAsync()
    {
        var users = await _userRepository.GetAllAsync();

        var usersDto = users
            .Select(u => _mapper.Map<User, UserReadDto>(u));
        
        return usersDto;
    }

    public async Task<UserReadDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null) 
            throw new KeyNotFoundException($"User could not be found, id = {id}");
        
        return _mapper.Map<UserReadDto>(user);
    }

    public async Task<UserReadDto> CreateAsync(UserCreateDto userCreateDto)
    {
        var userToCreate = _mapper.Map<User>(userCreateDto);

        userToCreate.PasswordHash = _passwordHasher.HashPassword(userToCreate, userCreateDto.Password);

        var createdUser = await _userRepository.CreateAsync(userToCreate);
        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserReadDto>(createdUser);
    }

    public async Task<UserReadDto> UpdateAsync(Guid id, UserUpdateDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null) 
            throw new KeyNotFoundException($"User could not be found, id = {id}");
        
        _mapper.Map(userDto, user);
        
        _passwordHasher.HashPassword(user, userDto.Password);
        
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        
        return _mapper.Map<UserReadDto>(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var userToDelete = await _userRepository.GetByIdAsync(id);
        
        if (userToDelete == null) 
            throw new KeyNotFoundException($"User could not be found, id = {id}");
        
        _userRepository.Delete(userToDelete);
        await _userRepository.SaveChangesAsync();
    }
}
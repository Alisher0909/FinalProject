using SkillHub.DTOs;
using SkillHub.DTOs.User;

namespace SkillHub.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<string> LoginAsync(LoginDto loginDto);
    Task<List<UserDto>> GetAllUsersAsync();
}
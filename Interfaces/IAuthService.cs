using SkillHub.DTOs;

namespace SkillHub.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<string> LoginAsync(LoginDto loginDto);
}
using SkillHub.DTOs;
using SkillHub.Models;

namespace SkillHub.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<string> LoginAsync(LoginDto loginDto);
    string GenerateToken(User user);
}
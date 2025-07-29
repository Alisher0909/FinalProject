using Microsoft.EntityFrameworkCore;
using SkillHub.Data;
using SkillHub.DTOs;
using SkillHub.Interfaces;
using SkillHub.Models;

namespace SkillHub.Services;

public class AuthService(SkillHubDbContext context, IConfiguration config, ITokenService tokenService) : IAuthService
{
    private readonly SkillHubDbContext _context = context;
    private readonly IConfiguration _config = config;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
        if (exists) throw new Exception("Email already exists");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _tokenService.GenerateToken(user);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid email or password");

        return _tokenService.GenerateToken(user);
    }
}
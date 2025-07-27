using SkillHub.Enums;

namespace SkillHub.DTOs;

public class RegisterDto
{
    public required string Name { get; set; } = null!;
    public required string Email { get; set; } = null!;
    public required string Password { get; set; } = null!;
    public required Role Role { get; set; }
}
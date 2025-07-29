using SkillHub.Enums;

namespace SkillHub.DTOs;

public class RegisterDto
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public required Role Role { get; set; }
}
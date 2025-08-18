using SkillHub.Enums;

namespace SkillHub.DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Bio { get; set; }
    public Role Role { get; set; }
}
namespace SkillHub.DTOs.User;

public class UpdateProfileDto
{
    public required string FullName { get; set; } = null!;
    public required string Email { get; set; } = null!;
    public string? Bio { get; set; } = ".Net Developer";
}
namespace SkillHub.DTOs.Users;

public class DeactivateUserDto
{
    public int ByUserId { get; set; }
    public string Reason { get; set; } = string.Empty;
}
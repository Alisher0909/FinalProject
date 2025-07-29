namespace SkillHub.DTOs.Users;

public class ActivateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime? ActivatedAt { get; set; }
    public int? ActivatedBy { get; set; }
}
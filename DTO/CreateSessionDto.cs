using SkillHub.Enums;

namespace SkillHub.DTOs.Session;

public class CreateSessionDto
{
    public required string Title { get; set; } = null!;
    public required string Description { get; set; } = null!;
    public string Tags { get; set; } = "";
    public required DifficultyLevel Difficulty { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required int Capacity { get; set; }
}
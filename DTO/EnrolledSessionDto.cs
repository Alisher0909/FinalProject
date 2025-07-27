using SkillHub.Enums;

namespace SkillHub.DTOs.Enrollment;

public class EnrolledSessionDto
{
    public int SessionId { get; set; }
    public string Title { get; set; } = null!;
    public string MentorName { get; set; } = null!;
    public DifficultyLevel Difficulty { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
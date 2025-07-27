namespace SkillHub.DTOs.Reviews;

public class CreateReviewDto
{
    public int SessionId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
}
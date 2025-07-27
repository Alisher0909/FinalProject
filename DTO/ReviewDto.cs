namespace SkillHub.DTOs.Reviews;

public class ReviewDto
{
    public string LearnerName { get; set; } = null!;
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
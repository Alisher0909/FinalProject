namespace SkillHub.Models;

public class Review
{
    public int Id { get; set; }

    public int LearnerId { get; set; }
    public User Learner { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;

    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
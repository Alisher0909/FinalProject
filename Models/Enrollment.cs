namespace SkillHub.Models;

public class Enrollment
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int LearnerId { get; set; }
    public User Learner { get; set; } = null!;

    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;

    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
}
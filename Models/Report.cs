namespace SkillHub.Models;

public class Report
{
    public int Id { get; set; }

    public int ReporterId { get; set; }
    public User Reporter { get; set; } = null!;

    public int? SessionId { get; set; }
    public Session? Session { get; set; }

    public int? ReviewId { get; set; }
    public Review? Review { get; set; }

    public string Reason { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Message { get; set; }


}
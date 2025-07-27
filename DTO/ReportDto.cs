namespace SkillHub.DTOs.Reports;

public class ReportDto
{
    public int Id { get; set; }
    public string Reporter { get; set; } = null!;
    public string? SessionTitle { get; set; }
    public string? ReviewComment { get; set; }
    public string Reason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public required string Message { get; set; }
}
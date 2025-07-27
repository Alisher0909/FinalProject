namespace SkillHub.DTOs.Reports;

public class CreateReportDto
{
    public int? SessionId { get; set; }
    public int? ReviewId { get; set; }
    public string Reason { get; set; } = null!;
    public required string Message { get; set; }
}
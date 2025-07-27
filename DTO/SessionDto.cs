namespace SkillHub.DTOs.Sessions;

public class SessionDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Tags { get; set; }
    public required string Difficulty { get; set; }
    public DateTime TimeFrame { get; set; }
    public required int Capacity { get; set; }
    public string MentorId { get; set; } = null!;
    public required string MentorName { get; set; }
    public string MentorAvatar { get; set; } = null!;
}
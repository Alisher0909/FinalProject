namespace SkillHub.DTOs;

public class UpdateSessionDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Tags { get; set; }
    public required string Difficulty { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Capacity { get; set; }
}
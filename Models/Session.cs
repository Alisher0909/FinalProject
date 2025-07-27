using SkillHub.Enums;

namespace SkillHub.Models;

public class Session
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Tags { get; set; } = null!;
    public DifficultyLevel Difficulty { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Capacity { get; set; }

    public int MentorId { get; set; }
    public User Mentor { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<UploadedFile> UploadedFiles { get; set; } = [];
    public ICollection<Report> Reports { get; set; } = [];
}
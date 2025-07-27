using SkillHub.Enums;

namespace SkillHub.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public required string Email { get; set; } = null!;
    public required string PasswordHash { get; set; } = null!;
    public string? Bio { get; set; }
    public Role Role { get; set; }

    public bool IsActive { get; set; } = true;

    public int? ActivatedBy { get; set; }
    public DateTime? ActivatedAt { get; set; }
    public int? DeactivatedBy { get; set; }
    public DateTime? DeactivatedAt { get; set; }
    public string? DeactivationReason { get; set; }

    public ICollection<Session> MentoredSessions { get; set; } = [];
    public ICollection<Enrollment> Enrollments { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Report> Reports { get; set; } = [];
    public ICollection<UploadedFile> UploadedFiles { get; set; } = [];
}
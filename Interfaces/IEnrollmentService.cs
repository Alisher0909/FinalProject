using SkillHub.DTOs.Enrollment;

namespace SkillHub.Interfaces;

public interface IEnrollmentService
{
    Task<List<EnrolledSessionDto>> SearchSessionsAsync(string? keyword);
    Task<bool> EnrollAsync(int userId, int sessionId);
    Task<bool> UnenrollAsync(int userId, int sessionId);
    Task<List<EnrolledSessionDto>> GetMyEnrollmentsAsync(int userId);
}
using SkillHub.DTOs.Reviews;

namespace SkillHub.Interfaces;

public interface IReviewService
{
    Task<bool> AddReviewAsync(int userId, CreateReviewDto dto);
    Task<List<ReviewDto>> GetSessionReviewsAsync(int sessionId);
    Task<bool> DeleteReviewAsync(int userId, int reviewId);
}
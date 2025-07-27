using Microsoft.EntityFrameworkCore;
using SkillHub.Data;
using SkillHub.DTOs.Reviews;
using SkillHub.Interfaces;

namespace SkillHub.Services;

public class ReviewService(SkillHubDbContext context) : IReviewService
{
    private readonly SkillHubDbContext _context = context;

    public async Task<bool> AddReviewAsync(int userId, CreateReviewDto dto)
    {
        var enrolled = await _context.Enrollments
            .AnyAsync(e => e.UserId == userId && e.SessionId == dto.SessionId);

        if (!enrolled) return false;

        var alreadyReviewed = await _context.Reviews
            .AnyAsync(r => r.UserId == userId && r.SessionId == dto.SessionId);

        if (alreadyReviewed) return false;

        var review = new Models.Review
        {
            UserId = userId,
            SessionId = dto.SessionId,
            Rating = dto.Rating,
            Comment = dto.Comment
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ReviewDto>> GetSessionReviewsAsync(int sessionId)
    {
        return await _context.Reviews
            .Where(r => r.SessionId == sessionId)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewDto
            {
                LearnerName = r.User.Name,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToListAsync();
    }

    public async Task<bool> DeleteReviewAsync(int userId, int reviewId)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);

        if (review == null) return false;

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }
}
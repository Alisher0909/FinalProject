using Microsoft.EntityFrameworkCore;
using SkillHub.Data;
using SkillHub.DTOs.Enrollment;
using SkillHub.Interfaces;

namespace SkillHub.Services;

public class EnrollmentService(SkillHubDbContext context) : IEnrollmentService
{
    private readonly SkillHubDbContext _context = context;

    public async Task<List<EnrolledSessionDto>> SearchSessionsAsync(string? keyword)
    {
        var query = _context.Sessions.Include(s => s.Mentor).AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(s =>
                s.Title.Contains(keyword) ||
                s.Description.Contains(keyword) ||
                s.Tags.Contains(keyword));
        }

        return await query.Select(s => new EnrolledSessionDto
        {
            SessionId = s.Id,
            Title = s.Title,
            MentorName = s.Mentor.Name,
            Difficulty = s.Difficulty,
            StartDate = s.StartDate,
            EndDate = s.EndDate
        }).ToListAsync();
    }

    public async Task<bool> EnrollAsync(int userId, int sessionId)
    {
        var already = await _context.Enrollments
            .AnyAsync(e => e.UserId == userId && e.SessionId == sessionId);

        if (already) return false;

        _context.Enrollments.Add(new Models.Enrollment
        {
            UserId = userId,
            SessionId = sessionId
        });

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnenrollAsync(int userId, int sessionId)
    {
        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.UserId == userId && e.SessionId == sessionId);

        if (enrollment == null) return false;

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<EnrolledSessionDto>> GetMyEnrollmentsAsync(int userId)
    {
        return await _context.Enrollments
            .Where(e => e.UserId == userId)
            .Include(e => e.Session)
            .ThenInclude(s => s.Mentor)
            .Select(e => new EnrolledSessionDto
            {
                SessionId = e.SessionId,
                Title = e.Session.Title,
                MentorName = e.Session.Mentor.Name,
                Difficulty = e.Session.Difficulty,
                StartDate = e.Session.StartDate,
                EndDate = e.Session.EndDate
            }).ToListAsync();
    }
}

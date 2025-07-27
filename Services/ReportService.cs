using Microsoft.EntityFrameworkCore;
using SkillHub.Data;
using SkillHub.DTOs.Reports;
using SkillHub.Interfaces;

namespace SkillHub.Services;

public class ReportService(SkillHubDbContext context) : IReportService
{
    private readonly SkillHubDbContext _context = context;

    public async Task<bool> SubmitReportAsync(int userId, CreateReportDto dto)
    {
        if (dto.SessionId == null && dto.ReviewId == null) return false;

        var report = new Models.Report
        {
            ReporterId = userId,
            SessionId = dto.SessionId,
            ReviewId = dto.ReviewId,
            Reason = dto.Reason,
            Message = dto.Message
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ReportDto>> GetAllReportsAsync()
    {
        return await _context.Reports
            .Include(r => r.Reporter)
            .Include(r => r.Session)
            .Include(r => r.Review)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReportDto
            {
                Id = r.Id,
                Reporter = r.Reporter.Name,
                SessionTitle = r.Session != null ? r.Session.Title : null,
                ReviewComment = r.Review != null ? r.Review.Comment : null,
                Reason = r.Reason,
                CreatedAt = r.CreatedAt,
                Message = r.Message!
            }).ToListAsync();
    }

    public async Task<bool> DeleteReportAsync(int id)
    {
        var report = await _context.Reports.FindAsync(id);
        if (report == null) return false;

        _context.Reports.Remove(report);
        await _context.SaveChangesAsync();
        return true;
    }
}
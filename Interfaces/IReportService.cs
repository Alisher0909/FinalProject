using SkillHub.DTOs.Reports;

namespace SkillHub.Interfaces;

public interface IReportService
{
    Task<bool> SubmitReportAsync(int userId, CreateReportDto dto);
    Task<List<ReportDto>> GetAllReportsAsync();
    Task<bool> DeleteReportAsync(int id);
}
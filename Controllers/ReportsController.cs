using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.DTOs.Reports;
using SkillHub.Interfaces;
using System.Security.Claims;

namespace SkillHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController(IReportService reportService) : ControllerBase
{
    private readonly IReportService _reportService = reportService;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    [Authorize(Policy = "LearnerOrMentor")]
    public async Task<IActionResult> Submit(CreateReportDto dto)
    {
        var success = await _reportService.SubmitReportAsync(GetUserId(), dto);
        if (!success)
            return BadRequest("Must be a SessionId or ReviewId");
        return Ok("Report sent");
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _reportService.GetAllReportsAsync();
        return Ok(reports);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _reportService.DeleteReportAsync(id);
        if (!success)
            return NotFound();
        return Ok("Report deleted");
    }
}
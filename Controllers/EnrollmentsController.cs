using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Interfaces;
using System.Security.Claims;

namespace SkillHub.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "LearnerOnly")]
public class EnrollmentsController(IEnrollmentService enrollmentService) : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService = enrollmentService;

    private int GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(id))
            throw new UnauthorizedAccessException("User ID not found in token.");

        return int.Parse(id);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return BadRequest("Keyword cannot be empty.");
        }
        
        var results = await _enrollmentService.SearchSessionsAsync(keyword);
        return Ok(results);
    }

    [HttpPost("{sessionId}")]
    public async Task<IActionResult> Enroll(int sessionId)
    {
        var success = await _enrollmentService.EnrollAsync(GetUserId(), sessionId);
        if (!success) return BadRequest($"You are already enrolled in session {sessionId}.");
        return Ok("Enrolled");
    }

    [HttpDelete("{sessionId}")]
    public async Task<IActionResult> Unenroll(int sessionId)
    {
        var success = await _enrollmentService.UnenrollAsync(GetUserId(), sessionId);
        if (!success) return NotFound("Not enrolled.");
        return Ok("Unenrolled");
    }

    [HttpGet("my")]
    public async Task<IActionResult> MyEnrollments()
    {
        var result = await _enrollmentService.GetMyEnrollmentsAsync(GetUserId());
        return Ok(result);
    }
}
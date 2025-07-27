using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.DTOs;
using SkillHub.DTOs.Session;
using SkillHub.Interfaces;
using System.Security.Claims;

namespace SkillHub.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Mentor")]
public class SessionsController(ISessionService sessionService) : ControllerBase
{
    private readonly ISessionService _sessionService = sessionService;

    private int GetMentorId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    public async Task<IActionResult> Create(CreateSessionDto dto)
    {
        var result = await _sessionService.CreateSessionAsync(GetMentorId(), dto);
        return Ok(result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMySessions()
    {
        var sessions = await _sessionService.GetMySessionsAsync(GetMentorId());
        return Ok(sessions);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSessionDto dto)
    {
        var result = await _sessionService.UpdateSessionAsync(id, GetMentorId(), dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _sessionService.DeleteSessionAsync(id, GetMentorId());
        if (!success) return NotFound("Session not found or unauthorized.");
        return Ok("Deleted");
    }
}
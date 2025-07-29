using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.DTOs.User;
using SkillHub.DTOs.Users;
using SkillHub.Interfaces;

namespace SkillHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMyProfile()
    {
        var result = await _userService.GetMyProfileAsync(GetUserId());

        if (result == null)
            return NotFound(new { Message = "User profile not found." });

        return Ok(new
        {
            Message = "User profile retrieved successfully.",
            User = result,
        });
    }

    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(dto.FullName) || string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest(new { Message = "Full name and email are required." });

        var result = await _userService.UpdateMyProfileAsync(GetUserId(), dto);
        return Ok(result);
    }

    [HttpPost("{userId}/activate")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> ActivateUser(int userId)
    {
        try
        {
            var result = await _userService.ActivateUserAsync(userId);
            return Ok(new
            {
                Message = "User activated successfully.",
                User = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("{userId}/deactivate")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeactivateUser(int userId, [FromBody] DeactivateUserDto dto)
    {
        if (!ModelState.IsValid || dto == null || dto.ByUserId <= 0)
            return BadRequest(new { Message = "Invalid deactivation data." });

        var result = await _userService.DeactivateUserAsync(userId, dto);
        return Ok(result);
    }
}
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

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        var result = await _userService.GetMyProfileAsync(userId);

        if (result == null)
        {
            return NotFound(new { Message = "User profile not found." });
        }

        return Ok(new
        {
            Message = "User profile retrieved successfully.",
            User = result,
        });  
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (dto == null)
        {
            return BadRequest(new { Message = "Profile data is required." });
        }

        if (string.IsNullOrWhiteSpace(dto.FullName) || string.IsNullOrWhiteSpace(dto.Email))
        {
            return BadRequest(new { Message = "Full name and email are required." });
        }

        int userId = int.Parse(User.FindFirst("id")!.Value);
        var result = await _userService.UpdateMyProfileAsync(userId, dto);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{userId}/activate")]
    public async Task<IActionResult> ActivateUser(int userId, [FromBody] ActivateUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (dto == null)
        {
            return BadRequest(new { Message = "Activation data is required." });
        }

        if (dto.ByUserId <= 0)
        {
            return BadRequest(new { Message = "Invalid user ID for activation." });
        }

        var result = await _userService.ActivateUserAsync(userId, dto);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{userId}/deactivate")]
    public async Task<IActionResult> DeactivateUser(int userId, [FromBody] DeactivateUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (dto == null)
        {
            return BadRequest(new { Message = "Deactivation data is required." });
        }

        if (dto.ByUserId <= 0)
        {
            return BadRequest(new { Message = "Invalid user ID for deactivation." });
        }
        
        var result = await _userService.DeactivateUserAsync(userId, dto);
        return Ok(result);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.DTOs.Reviews;
using SkillHub.Interfaces;
using System.Security.Claims;

namespace SkillHub.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Learner")]
public class ReviewsController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    public async Task<IActionResult> Add(CreateReviewDto dto)
    {
        var success = await _reviewService.AddReviewAsync(GetUserId(), dto);
        if (!success) return BadRequest("Cannot leave review. Check enrollment or existing review.");
        return Ok("Review submitted");
    }

    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetBySession(int sessionId)
    {
        var reviews = await _reviewService.GetSessionReviewsAsync(sessionId);
        return Ok(reviews);
    }

    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> Delete(int reviewId)
    {
        var success = await _reviewService.DeleteReviewAsync(GetUserId(), reviewId);
        if (!success) return NotFound("Review not found or unauthorized.");
        return Ok("Review deleted");
    }
}
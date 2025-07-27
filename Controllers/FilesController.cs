using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Interfaces;
using System.Security.Claims;

namespace SkillHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController(IFileService fileService) : ControllerBase
{
    private readonly IFileService _fileService = fileService;

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("upload")]
    [Authorize(Roles = "Mentor")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Fayl tanlanmagan!");

        var result = await _fileService.UploadAsync(GetUserId(), file);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Mentor")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _fileService.DeleteAsync(id);
        return success ? Ok("Fayl o'chirildi") : NotFound("Fayl topilmadi");
    }
}
using SkillHub.Data;
using SkillHub.Models;
using SkillHub.DTOs.Reports;
using SkillHub.DTOs.Users;
using SkillHub.DTOs.User;
using SkillHub.Interfaces;
using System.Security.Claims;

namespace SkillHub.Services;

public class UserService(SkillHubDbContext context, IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly SkillHubDbContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<UserDto> GetMyProfileAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId)
            ?? throw new Exception("User not found");

        return new UserDto
        {
            Id = user.Id,
            FullName = user.Name,
            Email = user.Email,
            Bio = user.Bio,
            Role = user.Role
        };
    }

    public async Task<UserDto> UpdateMyProfileAsync(int userId, UpdateProfileDto dto)
    {
        var user = await _context.Users.FindAsync(userId)
            ?? throw new Exception("User not found");

        user.Name = dto.FullName;
        user.Email = dto.Email;
        user.Bio = dto.Bio;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Id = user.Id,
            FullName = user.Name,
            Email = user.Email,
            Bio = user.Bio,
            Role = user.Role
        };
    }

    public async Task<ActivateDto> ActivateUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found.");

        if (user.IsActive)
            throw new Exception("User is already active.");

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(currentUserId))
            throw new Exception("Unauthorized access.");

        user.IsActive = true;
        user.ActivatedAt = DateTime.UtcNow;
        user.ActivatedBy = int.Parse(currentUserId!);

        await _context.SaveChangesAsync();

        return new ActivateDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString(),
            IsActive = user.IsActive,
            ActivatedAt = user.ActivatedAt,
            ActivatedBy = user.ActivatedBy
        };
    }

    public async Task<ReportDto> DeactivateUserAsync(int userId, DeactivateUserDto dto)
    {
        var user = await _context.Users.FindAsync(userId)
            ?? throw new Exception("User not found");

        user.IsActive = false;
        user.DeactivatedAt = DateTime.UtcNow;
        user.DeactivatedBy = dto.ByUserId;
        user.DeactivationReason = dto.Reason;

        var report = new Report
        {
            Message = $"User {user.Name} has been deactivated",
            CreatedAt = DateTime.UtcNow,
            ReporterId = dto.ByUserId,   
            Reason = dto.Reason           
        };

        _context.Reports.Add(report);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Save xatoligi: " + ex.InnerException?.Message ?? ex.Message);
        }

        return new ReportDto
        {
            Id = report.Id,
            Message = report.Message,
            CreatedAt = report.CreatedAt
        };
    }
}
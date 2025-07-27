using SkillHub.Data;
using SkillHub.Models;
using SkillHub.DTOs.Reports;
using SkillHub.DTOs.Users;
using SkillHub.DTOs.User;
using SkillHub.Interfaces;

namespace SkillHub.Services;

public class UserService(SkillHubDbContext context) : IUserService
{
    private readonly SkillHubDbContext _context = context;

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

    public async Task<ReportDto> ActivateUserAsync(int userId, ActivateUserDto dto)
    {
        var user = await _context.Users.FindAsync(userId)
            ?? throw new Exception("User not found");

        user.IsActive = true;
        user.ActivatedAt = DateTime.UtcNow;
        user.ActivatedBy = dto.ByUserId;

        var report = new Report
        {
            Message = $"User {user.Name} is now active",
            CreatedAt = DateTime.UtcNow
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return new ReportDto
        {
            Id = report.Id,
            Message = report.Message,
            CreatedAt = report.CreatedAt
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
            CreatedAt = DateTime.UtcNow
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return new ReportDto
        {
            Id = report.Id,
            Message = report.Message,
            CreatedAt = report.CreatedAt
        };
    }
}
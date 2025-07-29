using SkillHub.DTOs.Reports;
using SkillHub.DTOs.User;
using SkillHub.DTOs.Users;

namespace SkillHub.Interfaces;

public interface IUserService
{
    Task<UserDto> GetMyProfileAsync(int userId);
    Task<UserDto> UpdateMyProfileAsync(int userId, UpdateProfileDto dto);
    Task<ReportDto> DeactivateUserAsync(int userId, DeactivateUserDto dto);
    Task<ActivateDto> ActivateUserAsync(int userId);
}
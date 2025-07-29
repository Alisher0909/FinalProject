using SkillHub.Models;

namespace SkillHub.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
using SkillHub.DTOs;
using SkillHub.DTOs.Session;
using SkillHub.DTOs.Sessions;

namespace SkillHub.Interfaces;

public interface ISessionService
{
    Task<SessionDto> CreateSessionAsync(int mentorId, CreateSessionDto dto);
    Task<List<SessionDto>> GetMySessionsAsync(int mentorId);
    Task<SessionDto> UpdateSessionAsync(int sessionId, int mentorId, UpdateSessionDto dto);
    Task<bool> DeleteSessionAsync(int sessionId, int mentorId);
}
using Microsoft.EntityFrameworkCore;
using SkillHub.Data;
using SkillHub.DTOs;
using SkillHub.DTOs.Session;
using SkillHub.DTOs.Sessions;
using SkillHub.Models;
using SkillHub.Interfaces;

namespace SkillHub.Services;

public class SessionService(SkillHubDbContext context) : ISessionService
{
    private readonly SkillHubDbContext _context = context;

    public async Task<SessionDto> CreateSessionAsync(int mentorId, CreateSessionDto dto)
    {
        var session = new Session
        {
            Title = dto.Title,
            Description = dto.Description,
            Tags = dto.Tags,
            Difficulty = dto.Difficulty,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Capacity = dto.Capacity,
            MentorId = mentorId
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        return MapToDto(session);
    }

    public async Task<List<SessionDto>> GetMySessionsAsync(int mentorId)
    {
        var sessions = await _context.Sessions
            .Where(s => s.MentorId == mentorId)
            .Include(s => s.Mentor)
            .ToListAsync();

        return sessions.Select(MapToDto).ToList();
    }

    public async Task<SessionDto> UpdateSessionAsync(int sessionId, int mentorId, UpdateSessionDto dto)
    {
        var session = await _context.Sessions
            .Include(s => s.Mentor)
            .FirstOrDefaultAsync(s => s.Id == sessionId)
            ?? throw new Exception("Session not found");

        session.Title = dto.Title;
        session.Description = dto.Description;
        session.Tags = dto.Tags;
        session.StartDate = dto.StartDate;
        session.EndDate = dto.EndDate;
        session.Capacity = dto.Capacity;


        await _context.SaveChangesAsync();

        return new SessionDto
        {
            Id = session.Id,
            Title = session.Title,
            Description = session.Description,
            Tags = session.Tags,
            Difficulty = session.Difficulty.ToString(),
            TimeFrame = session.StartDate,
            Capacity = session.Capacity,
            MentorId = session.MentorId.ToString(),
            MentorName = session.Mentor.Name,
            MentorAvatar = session.Mentor.AvatarUrl ?? string.Empty
        };
    }

    public async Task<bool> DeleteSessionAsync(int sessionId, int mentorId)
    {
        var session = await _context.Sessions
            .FirstOrDefaultAsync(s => s.Id == sessionId && s.MentorId == mentorId);

        if (session == null) return false;

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
        return true;
    }

    private static SessionDto MapToDto(Session s)
    {
        return new SessionDto
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            Tags = s.Tags,
            Difficulty = s.Difficulty.ToString(),
            TimeFrame = s.StartDate,
            Capacity = s.Capacity,
            MentorId = s.MentorId.ToString(),
            MentorName = s.Mentor.Name,
            MentorAvatar = s.Mentor.AvatarUrl ?? string.Empty
        };
    }
}
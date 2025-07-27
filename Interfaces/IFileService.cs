using SkillHub.DTOs.Files;

namespace SkillHub.Interfaces;

public interface IFileService
{
    Task<FileUploadResultDto> UploadAsync(int mentorId, IFormFile file);
    Task<bool> DeleteAsync(int fileId);
}
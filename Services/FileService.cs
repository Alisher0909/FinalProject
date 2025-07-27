using SkillHub.DTOs.Files;
using SkillHub.Interfaces;
using SkillHub.Models;
using SkillHub.Data;

namespace SkillHub.Services;

public class FileService(SkillHubDbContext context, IWebHostEnvironment env) : IFileService
{
    private readonly SkillHubDbContext _context = context;
    private readonly IWebHostEnvironment _env = env;

    public async Task<FileUploadResultDto> UploadAsync(int mentorId, IFormFile file)
    {
        var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var upload = new UploadedFile
        {
            MentorId = mentorId,
            FileName = file.FileName,
            FilePath = "/uploads/" + fileName
        };

        _context.UploadedFiles.Add(upload);
        await _context.SaveChangesAsync();

        return new FileUploadResultDto
        {
            FileName = file.FileName,
            FileUrl = upload.FilePath
        };
    }

    public async Task<bool> DeleteAsync(int fileId)
    {
        var file = await _context.UploadedFiles.FindAsync(fileId);
        if (file == null) return false;

        var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", file.FilePath.TrimStart('/'));
        if (File.Exists(fullPath)) File.Delete(fullPath);

        _context.UploadedFiles.Remove(file);
        await _context.SaveChangesAsync();

        return true;
    }
}

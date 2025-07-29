using Microsoft.EntityFrameworkCore;
using SkillHub.Models;

namespace SkillHub.Data;

public class SkillHubDbContext : DbContext
{
    public SkillHubDbContext(DbContextOptions<SkillHubDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<UploadedFile> UploadedFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enrollment konfiguratsiyasi
        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.UserId, e.SessionId });

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Session)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Session ↔ Mentor
        modelBuilder.Entity<Session>()
            .HasOne(s => s.Mentor)
            .WithMany(u => u.MentoredSessions)
            .HasForeignKey(s => s.MentorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Review ↔ Session
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Session)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Review ↔ Learner
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Learner)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.LearnerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Report ↔ Session
        modelBuilder.Entity<Report>()
            .HasOne(r => r.Session)
            .WithMany(s => s.Reports)
            .HasForeignKey(r => r.SessionId)
            .OnDelete(DeleteBehavior.SetNull);

        // Report ↔ Reporter (User)
        modelBuilder.Entity<Report>()
            .HasOne(r => r.Reporter)
            .WithMany(u => u.Reports)
            .HasForeignKey(r => r.ReporterId)
            .OnDelete(DeleteBehavior.Cascade);

        // UploadedFile ↔ User
        modelBuilder.Entity<UploadedFile>()
            .HasOne(f => f.User)
            .WithMany(u => u.UploadedFiles)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using SkillHub.Enums;
using SkillHub.Models;

namespace SkillHub.Data;

public static class SeedData
{
    public static void Initialize(SkillHubDbContext context)
    {
        if (!context.Users.Any())
        {
            var admin = new User
            {
                Name = "Admin User",
                Email = "admin@skillhub.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = Role.Admin,
                IsActive = true
            };

            var mentor = new User
            {
                Name = "Mentor John",
                Email = "mentor@skillhub.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("mentor123"),
                Role = Role.Mentor,
                Bio = "Experienced .NET Developer",
                IsActive = true
            };

            var learner = new User
            {
                Name = "Learner Alice",
                Email = "learner@skillhub.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("learner123"),
                Role = Role.Learner,
                IsActive = true
            };

            context.Users.AddRange(admin, mentor, learner);
            context.SaveChanges();

            var session1 = new Session
            {
                Title = "C# for Beginners",
                Description = "Learn the basics of C#",
                Tags = "C#, .NET, Beginner",
                Difficulty = DifficultyLevel.Beginner,
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(10),
                Capacity = 10,
                MentorId = mentor.Id
            };

            var session2 = new Session
            {
                Title = "Backend Development",
                Description = "Learn backend with .NET Web API and EF Core",
                Tags = "backend,dotnet,api",
                Difficulty = DifficultyLevel.Intermediate,
                StartDate = DateTime.UtcNow.AddDays(5),
                EndDate = DateTime.UtcNow.AddDays(15),
                Capacity = 15,
                MentorId = mentor.Id
            };

            context.Sessions.AddRange(session1, session2);
            context.SaveChanges();

            var enrollment = new Enrollment
            {
                SessionId = session1.Id,
                UserId = learner.Id,
                EnrolledAt = DateTime.UtcNow
            };

            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            var review = new Review
            {
                SessionId = session1.Id,
                UserId = learner.Id,
                Rating = 5,
                Comment = "Great session, learned a lot!",
                CreatedAt = DateTime.UtcNow
            };

            context.Reviews.Add(review);
            context.SaveChanges();
        }
    }
}
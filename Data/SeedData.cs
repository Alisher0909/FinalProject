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
                Role = Role.Admin
            };

            var mentor = new User
            {
                Name = "Mentor John",
                Email = "mentor@skillhub.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("mentor123"),
                Role = Role.Mentor,
                Bio = "Experienced .NET Developer"
            };

            var learner = new User
            {
                Name = "Learner Alice",
                Email = "learner@skillhub.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("learner123"),
                Role = Role.Learner
            };

            context.Users.AddRange(admin, mentor, learner);
            context.SaveChanges();

            var session = new Session
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

            context.Sessions.Add(session);
            context.SaveChanges();

            var enrollment = new Enrollment
            {
                SessionId = session.Id,
                UserId = learner.Id, 
                EnrolledAt = DateTime.UtcNow
            };

            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            var review = new Review
            {
                SessionId = session.Id,
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
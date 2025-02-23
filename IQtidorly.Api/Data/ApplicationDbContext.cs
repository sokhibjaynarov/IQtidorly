using IQtidorly.Api.Models.AgeGroups;
using IQtidorly.Api.Models.BookAuthors;
using IQtidorly.Api.Models.Books;
using IQtidorly.Api.Models.QuestionOptions;
using IQtidorly.Api.Models.Questions;
using IQtidorly.Api.Models.QuizParticipants;
using IQtidorly.Api.Models.QuizQuestions;
using IQtidorly.Api.Models.QuizSubmissions;
using IQtidorly.Api.Models.Quizzes;
using IQtidorly.Api.Models.SubjectChapters;
using IQtidorly.Api.Models.Subjects;
using IQtidorly.Api.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;

namespace IQtidorly.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public const string SCHEMA_NAME = "iqtidorly";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(SCHEMA_NAME);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            modelBuilder.Entity<AgeGroup>()
                .Property(ag => ag.Translations)
                .HasColumnType("jsonb");

            modelBuilder
            .HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(JsonExtractPathText),
                new[] { typeof(string), typeof(string), typeof(string) }))
            .HasTranslation(args =>
                new SqlFunctionExpression(
                    "jsonb_extract_path_text",
                    typeof(string),
                    args));

        }

        public static string JsonExtractPathText(string jsonbData, string language, string property)
        => throw new NotSupportedException("Direct calls are not supported. This method is translated to SQL.");



        public DbSet<AgeGroup> AgeGroups { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectChapter> SubjectChapters { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizParticipant> QuizParticipants { get; set; }
        public DbSet<QuizSubmission> QuizSubmissions { get; set; }
    }
}

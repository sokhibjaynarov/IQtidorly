using IQtidorly.Api.Models.AgeGroups;
using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.BookAuthors;
using IQtidorly.Api.Models.Books;
using IQtidorly.Api.Models.SubjectChapters;
using IQtidorly.Api.Models.Subjects;
using IQtidorly.Api.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IQtidorly.Api.Data
{
    public static class ContextSeed
    {
        #region User Roles And Users
        private static Guid _adminUserRoleId = Guid.Parse("0d53743c-0fb9-479b-92b5-b8c1dd204558");
        private static Guid _teacherUserRoleId = Guid.Parse("9ff5fadb-0642-4966-815b-eed52cea5604");
        private static Guid _studentUserRoleId = Guid.Parse("68ad6df6-15ad-42da-8ede-47cf5952dd09");

        private static Guid _adminUserId = Guid.Parse("11275c55-4655-4788-b74e-25b6d44308dc");
        private static Guid _teacherUserId = Guid.Parse("73aa52f6-20c7-43ed-aa78-c45cc4ffc7c5");
        private static Guid _studentUserId = Guid.Parse("05d71dc2-c63f-4806-a461-f970b07f67dd");

        #endregion

        #region Subjects and Subject Chapters

        private static Guid _mathematicsSubjectId = Guid.Parse("f1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _physicsSubjectId = Guid.Parse("f2b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _chemistrySubjectId = Guid.Parse("f3b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");

        // Mathematics Chapters
        private static Guid _algebraChapterId = Guid.Parse("a1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _geometryChapterId = Guid.Parse("a2b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");

        // Physics Chapters
        private static Guid _mechanicsChapterId = Guid.Parse("b1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _opticsChapterId = Guid.Parse("b2b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");

        // Chemistry Chapters
        private static Guid _organicChemistryChapterId = Guid.Parse("c1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _inorganicChemistryChapterId = Guid.Parse("c2b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");

        #endregion

        #region Book Authors and Books

        // Book Authors
        private static Guid _jKRowlingAuthorId = Guid.Parse("d1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _georgeOrwellAuthorId = Guid.Parse("d2b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");

        // Books
        private static Guid _harryPotterBookId = Guid.Parse("e1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _animalFarmBookId = Guid.Parse("e2b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");

        #endregion

        #region Age Groups

        private static Guid _childrenAgeGroupId = Guid.Parse("a3b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _teenagersAgeGroupId = Guid.Parse("a4b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");
        private static Guid _adultsAgeGroupId = Guid.Parse("a5b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b");

        #endregion


        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //Seed Roles
            if (!roleManager.Roles.Any(roleManager => roleManager.Id == _adminUserRoleId))
                await roleManager.CreateAsync(new Role(_adminUserRoleId, Enums.Role.Admin.ToString()));

            if (!roleManager.Roles.Any(roleManager => roleManager.Id == _teacherUserRoleId))
                await roleManager.CreateAsync(new Role(_teacherUserRoleId, Enums.Role.Teacher.ToString()));

            if (!roleManager.Roles.Any(roleManager => roleManager.Id == _studentUserRoleId))
                await roleManager.CreateAsync(new Role(_studentUserRoleId, Enums.Role.Student.ToString()));
        }

        public static async Task SeedUsersAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //Seed Admin User
            var adminUser = new User
            {
                Id = _adminUserId,
                UserName = "admin",
                Email = "admin@iqtidorly.uz",
                FirstName = "Admin",
                LastName = "Adminov",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "+1234567890",
                SecurityStamp = _adminUserId.ToString()
            };
            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var adminUserWithSameEmail = await userManager.FindByEmailAsync(adminUser.Email);
                if (adminUserWithSameEmail == null)
                {
                    await userManager.CreateAsync(adminUser, "Admin@123");
                    await userManager.AddToRoleAsync(adminUser, Enums.Role.Admin.ToString());
                }
            }

            //Seed Teacher User
            var teacherUser = new User
            {
                Id = _teacherUserId,
                UserName = "teacher",
                Email = "teacher@iqtidorly.uz",
                FirstName = "Teacher",
                LastName = "Teacherson",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "+1234567891",
                SecurityStamp = _teacherUserId.ToString()
            };
            if (userManager.Users.All(u => u.Id != teacherUser.Id))
            {
                var teacherUserWithSameEmail = await userManager.FindByEmailAsync(teacherUser.Email);
                if (teacherUserWithSameEmail == null)
                {
                    await userManager.CreateAsync(teacherUser, "Teacher@123");
                    await userManager.AddToRoleAsync(teacherUser, Enums.Role.Teacher.ToString());
                }
            }

            //Seed Student User
            var studentUser = new User
            {
                Id = _studentUserId,
                UserName = "student",
                Email = "student@iqtidorly.uz",
                FirstName = "Student",
                LastName = "Studentov",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "+1234567892",
                SecurityStamp = _studentUserId.ToString()
            };
            if (userManager.Users.All(u => u.Id != studentUser.Id))
            {
                var studentUserWithSameEmail = await userManager.FindByEmailAsync(studentUser.Email);
                if (studentUserWithSameEmail == null)
                {
                    await userManager.CreateAsync(studentUser, "Student@123");
                    await userManager.AddToRoleAsync(studentUser, Enums.Role.Student.ToString());
                }
            }
        }

        public static async Task SeedSubjectsAndChaptersAsync(ApplicationDbContext context)
        {
            // Seed Subjects
            if (!context.Subjects.Any(s => s.SubjectId == _mathematicsSubjectId))
            {
                context.Subjects.Add(new Subject
                {
                    SubjectId = _mathematicsSubjectId,
                    Name = "Mathematics"
                });
            }

            if (!context.Subjects.Any(s => s.SubjectId == _physicsSubjectId))
            {
                context.Subjects.Add(new Subject
                {
                    SubjectId = _physicsSubjectId,
                    Name = "Physics"
                });
            }

            if (!context.Subjects.Any(s => s.SubjectId == _chemistrySubjectId))
            {
                context.Subjects.Add(new Subject
                {
                    SubjectId = _chemistrySubjectId,
                    Name = "Chemistry"
                });
            }

            // Seed Chapters for Mathematics
            if (!context.SubjectChapters.Any(c => c.SubjectChapterId == _algebraChapterId))
            {
                context.SubjectChapters.Add(new SubjectChapter
                {
                    SubjectChapterId = _algebraChapterId,
                    Name = "Algebra",
                    SubjectId = _mathematicsSubjectId
                });
            }

            if (!context.SubjectChapters.Any(c => c.SubjectChapterId == _geometryChapterId))
            {
                context.SubjectChapters.Add(new SubjectChapter
                {
                    SubjectChapterId = _geometryChapterId,
                    Name = "Geometry",
                    SubjectId = _mathematicsSubjectId
                });
            }

            // Seed Chapters for Physics
            if (!context.SubjectChapters.Any(c => c.SubjectChapterId == _mechanicsChapterId))
            {
                context.SubjectChapters.Add(new SubjectChapter
                {
                    SubjectChapterId = _mechanicsChapterId,
                    Name = "Mechanics",
                    SubjectId = _physicsSubjectId
                });
            }

            if (!context.SubjectChapters.Any(c => c.SubjectChapterId == _opticsChapterId))
            {
                context.SubjectChapters.Add(new SubjectChapter
                {
                    SubjectChapterId = _opticsChapterId,
                    Name = "Optics",
                    SubjectId = _physicsSubjectId
                });
            }

            // Seed Chapters for Chemistry
            if (!context.SubjectChapters.Any(c => c.SubjectChapterId == _organicChemistryChapterId))
            {
                context.SubjectChapters.Add(new SubjectChapter
                {
                    SubjectChapterId = _organicChemistryChapterId,
                    Name = "Organic Chemistry",
                    SubjectId = _chemistrySubjectId
                });
            }

            if (!context.SubjectChapters.Any(c => c.SubjectChapterId == _inorganicChemistryChapterId))
            {
                context.SubjectChapters.Add(new SubjectChapter
                {
                    SubjectChapterId = _inorganicChemistryChapterId,
                    Name = "Inorganic Chemistry",
                    SubjectId = _chemistrySubjectId
                });
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedBooksAndAuthorsAsync(ApplicationDbContext context)
        {
            // Seed Authors
            if (!context.BookAuthors.Any(a => a.BookAuthorId == _jKRowlingAuthorId))
            {
                context.BookAuthors.Add(new BookAuthor
                {
                    BookAuthorId = _jKRowlingAuthorId,
                    FirsName = "J.K.",
                    LastName = "Rowling"
                });
            }

            if (!context.BookAuthors.Any(a => a.BookAuthorId == _georgeOrwellAuthorId))
            {
                context.BookAuthors.Add(new BookAuthor
                {
                    BookAuthorId = _georgeOrwellAuthorId,
                    FirsName = "George",
                    LastName = "Orwell"
                });
            }

            // Seed Books
            if (!context.Books.Any(b => b.BookId == _harryPotterBookId))
            {
                context.Books.Add(new Book
                {
                    BookId = _harryPotterBookId,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "A young wizard's journey begins.",
                    BookAuthorId = _jKRowlingAuthorId,
                    TotalPages = 223,
                    Price = 19.99m
                });
            }

            if (!context.Books.Any(b => b.BookId == _animalFarmBookId))
            {
                context.Books.Add(new Book
                {
                    BookId = _animalFarmBookId,
                    Title = "Animal Farm",
                    Description = "A satirical allegory of Soviet totalitarianism.",
                    BookAuthorId = _georgeOrwellAuthorId,
                    TotalPages = 112,
                    Price = 9.99m
                });
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedAgeGroupsAsync(ApplicationDbContext context)
        {
            // Seed Age Groups
            if (!context.AgeGroups.Any(a => a.AgeGroupId == _childrenAgeGroupId))
            {
                context.AgeGroups.Add(new AgeGroup
                {
                    AgeGroupId = _childrenAgeGroupId,
                    Name = "Children",
                    MinAge = 5,
                    MaxAge = 15,
                    Translations = new AgeGroupTranslation
                    {
                        Name = new TranslationModel
                        {
                            uz_UZ = "Bolalar",
                            ru_RU = "Дети",
                            en_US = "Children",
                            kaa_UZ = "Bolalar"
                        }
                    }
                });
            }

            if (!context.AgeGroups.Any(a => a.AgeGroupId == _teenagersAgeGroupId))
            {
                context.AgeGroups.Add(new AgeGroup
                {
                    AgeGroupId = _teenagersAgeGroupId,
                    Name = "Teenagers",
                    MinAge = 16,
                    MaxAge = 24,
                    Translations = new AgeGroupTranslation
                    {
                        Name = new TranslationModel
                        {
                            uz_UZ = "Yoshlar",
                            ru_RU = "Подростки",
                            en_US = "Teenagers",
                            kaa_UZ = "Yoshlar"
                        }
                    }
                });
            }

            if (!context.AgeGroups.Any(a => a.AgeGroupId == _adultsAgeGroupId))
            {
                context.AgeGroups.Add(new AgeGroup
                {
                    AgeGroupId = _adultsAgeGroupId,
                    Name = "Adults",
                    MinAge = 25,
                    MaxAge = 45,
                    Translations = new AgeGroupTranslation
                    {
                        Name = new TranslationModel
                        {
                            uz_UZ = "O'rgalar",
                            ru_RU = "Взрослые",
                            en_US = "Adults",
                            kaa_UZ = "O'rgalar"
                        }
                    }
                });
            }

            await context.SaveChangesAsync();
        }
    }
}

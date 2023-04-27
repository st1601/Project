using Common.Enums;
using Common.Helpers;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<Thumb> Thumbs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(c => c.ToTable("Category"));

            builder.Entity<Idea>(i => i.ToTable("Idea"));

            builder.Entity<Comment>(c => c.ToTable("Comment"));

            builder.Entity<Thumb>(t => t.ToTable("Thumb"));

            builder.Entity<User>(u => u.ToTable("User"));

            builder.Entity<Event>(f => f.ToTable("Event"));

            builder.Entity<Notification>(n => n.ToTable("Notification"));

            builder.Entity<Thumb>()
                .HasOne(t => t.Idea)
                .WithMany(t => t.Thumbs)
                .HasForeignKey(t => t.IdeaId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<Thumb>()
                .HasOne(t => t.User)
                .WithMany(t => t.Thumbs)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<Idea>()
                .HasOne(i => i.User)
                .WithMany(i => i.Ideas)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<Idea>()
                .HasOne(i => i.Event)
                .WithMany(i => i.Ideas)
                .HasForeignKey(i => i.EventId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<Idea>()
                .HasMany(i => i.Categories)
                .WithMany(c => c.Ideas)
                .UsingEntity(i => i.ToTable("IdeaCategories"));

            builder.Entity<Comment>()
                .HasOne(c => c.Idea)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.IdeaId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<Notification>()
                .HasOne(n => n.Idea)
                .WithMany(n => n.Notifications)
                .HasForeignKey(n => n.IdeaId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Staff",
                    Password = HashStringHelper.HashString("Staff@123"),
                    FullName = "Staff",
                    Email = "tonydo0307@gmail.com",
                    PhoneNumber = 11112222,
                    Department = DepartmentEnum.IT,
                    Role = UserRoleEnum.Staff
                },
                new User
                {
                    Id = 2,
                    UserName = "Admin",
                    Password = HashStringHelper.HashString("Admin@123"),
                    FullName = "Admin",
                    Email = "tonydo0307@gmail.com",
                    PhoneNumber = 11112222,
                    Department = DepartmentEnum.None,
                    Role = UserRoleEnum.Admin
                },
                new User
                {
                    Id = 3,
                    UserName = "QAManager",
                    Password = HashStringHelper.HashString("QAManager@123"),
                    FullName = "QAManager",
                    Email = "tonydo0307@gmail.com",
                    PhoneNumber = 11112222,
                    Department = DepartmentEnum.None,
                    Role = UserRoleEnum.QAManager
                },
                new User
                {
                    Id = 4,
                    UserName = "QACoordinator",
                    Password = HashStringHelper.HashString("QACoordinator@123"),
                    FullName = "QACoordinator",
                    Email = "tonydo0307@gmail.com",
                    PhoneNumber = 11112222,
                    Department = DepartmentEnum.IT,
                    Role = UserRoleEnum.QACoordinator
                },
                new User
                {
                    Id = 5,
                    UserName = "QACoordinator1",
                    Password = HashStringHelper.HashString("123456"),
                    FullName = "QACoordinator1",
                    Email = "tonydo0307@gmail.com",
                    PhoneNumber = 11112222,
                    Department = DepartmentEnum.BusinessManagement,
                    Role = UserRoleEnum.QACoordinator
                },
                new User
                {
                    Id = 6,
                    UserName = "Staff1",
                    Password = HashStringHelper.HashString("123456"),
                    FullName = "Staff1",
                    Email = "tonydo0307@gmail.com",
                    PhoneNumber = 11112222,
                    Department = DepartmentEnum.BusinessManagement,
                    Role = UserRoleEnum.Staff
                }
                );

            builder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Python", CategoryDescription = "Back-end" },
                new Category { Id = 2, CategoryName = "ASP .Net Core", CategoryDescription = "Back-end" },
                new Category { Id = 3, CategoryName = "Angular", CategoryDescription = "Front-end" },
                new Category { Id = 4, CategoryName = "ReactJS", CategoryDescription = "Front-end" }
                );

            builder.Entity<Event>().HasData(
                new Event { Id = 1, EventName = "IT talk show", EventDescription = "Software engineer", FirstClosingDate = DateTime.Now, LastClosingDate = DateTime.MaxValue, UserId = 4 },
                new Event { Id = 2, EventName = "Business talk show", EventDescription = "Block Chain", FirstClosingDate = DateTime.Now, LastClosingDate = DateTime.MaxValue, UserId = 5 }
                );

            builder.Entity<Idea>().HasData(
                new { Id = 1, IdeaTitle = "Question", IdeaDescription = "What do you need to be a software engineer?", DateSubmitted = DateTime.Now, File = "Demo.docx", UserId = 1, EventId = 1, HashTag = "#IT" },
                new { Id = 2, IdeaTitle = "Question", IdeaDescription = "What do you need to do to understand blockchain?", DateSubmitted = DateTime.Now, File = "Demo.docx", UserId = 6, EventId = 2, HashTag = "#Business" }
                );

            builder.Entity<Idea>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Ideas)
                .UsingEntity(b => b.HasData(
                    new { CategoriesId = 2, IdeasId = 1 },
                    new { CategoriesId = 3, IdeasId = 1 },
                    new { CategoriesId = 4, IdeasId = 1 },
                    new { CategoriesId = 1, IdeasId = 2 },
                    new { CategoriesId = 3, IdeasId = 2 },
                    new { CategoriesId = 4, IdeasId = 2 }
            ));

            builder.Entity<Comment>().HasData(
                new { Id = 1, CommentContent = "string", DateSubmitted = DateTime.Now, UserId = 2, IdeaId = 1 },
                new { Id = 2, CommentContent = "string", DateSubmitted = DateTime.Now, UserId = 3, IdeaId = 1 },
                new { Id = 3, CommentContent = "string", DateSubmitted = DateTime.Now, UserId = 5, IdeaId = 1 },
                new { Id = 4, CommentContent = "string", DateSubmitted = DateTime.Now, UserId = 3, IdeaId = 2 },
                new { Id = 5, CommentContent = "string", DateSubmitted = DateTime.Now, UserId = 1, IdeaId = 2 }
                );

            builder.Entity<Notification>().HasData(
                new { Id = 1, NotificationName = "string 123", IdeaId = 1 },
                new { Id = 2, NotificationName = "string 1234", IdeaId = 2 }
                );
        }
    }
}
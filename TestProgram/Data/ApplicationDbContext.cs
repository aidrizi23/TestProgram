using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace TestProgram.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    public DbSet<TrueFalseQuestion> TrueFalseQuestions { get; set; }
    public DbSet<TextQuestion> TextQuestions { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Question>()
            .HasDiscriminator<string>("QuestionType")
            .HasValue<MultipleChoiceQuestion>("MultipleChoice")
            .HasValue<TextQuestion>("Text")
            .HasValue<TrueFalseQuestion>("TrueFalse");
        
        // create me a new static teacher with the id of 1
        modelBuilder.Entity<Teacher>().HasData(new Teacher
        {
            Id = "1",
            UserName = "teacher",
            NormalizedUserName = "TEACHER",
            Email = "albiidrizi@gmail.com",
            NormalizedEmail = "albiidrizi@gmail.com".ToUpper(),
            EmailConfirmed = true,
            FirstName = "Albi",
            LastName = "Idrizi",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
            PasswordHash = new PasswordHasher<Teacher>().HashPassword(null, "albiidrizi27"),
            TwoFactorEnabled = false,
            
        });
        
            
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        // Configuring the inheritance strategy (using TPH here)
        modelBuilder.Entity<Question>()
            .HasDiscriminator<string>("QuestionType")
            .HasValue<MultipleChoiceQuestion>("MultipleChoice")
            .HasValue<TextQuestion>("Text")
            .HasValue<TrueFalseQuestion>("TrueFalse");
    }
}
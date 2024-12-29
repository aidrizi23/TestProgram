using System.ComponentModel.DataAnnotations;
using TestProgram.Controllers;

namespace TestProgram.Data;

public class TestSubmission : BaseEntity
{
    public int TestId { get; set; }
    public Test Test { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string StudentFirstName { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string StudentLastName { get; set; }
        
    public DateTime SubmissionTime { get; set; }
    public float TotalScore { get; set; }
        
    public ICollection<StudentAnswer> Answers { get; set; }
}
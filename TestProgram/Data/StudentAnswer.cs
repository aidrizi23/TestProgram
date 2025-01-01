using System.ComponentModel.DataAnnotations;
using TestProgram.Data;

namespace TestProgram.Controllers;

public class StudentAnswer : BaseEntity
{
    public int TestSubmissionId { get; set; }
    public TestSubmission TestSubmission { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    [Required]
    public string Answer { get; set; } = string.Empty;
    public float Score { get; set; }
}
namespace TestProgram.Models.Question;

public abstract class QuestionForEditDto
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public int Points { get; set; }
    public int TestId { get; set; }
}

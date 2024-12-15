namespace TestProgram.Models.Question;

public abstract class QuestionForCreationDto
{
    public string QuestionText { get; set; }
    public int Points { get; set; }
    public int TestId { get; set; }
}
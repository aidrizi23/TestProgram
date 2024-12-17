namespace TestProgram.Models.Question;

public class TextQuestionForEditDto : QuestionForEditDto
{
    public List<string> ExpectedAnswer { get; set; } = new List<string>();
}
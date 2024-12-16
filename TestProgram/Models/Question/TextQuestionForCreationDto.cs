namespace TestProgram.Models.Question;

public class TextQuestionForCreationDto : QuestionForCreationDto
{
    public List<string> ExpectedAnswer { get; set; } = new List<string>();
}
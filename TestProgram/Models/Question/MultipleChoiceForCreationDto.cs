namespace TestProgram.Models.Question;

public class MultipleChoiceForCreationDto : QuestionForCreationDto
{
    public List<string> Options { get; set; } = new List<string>();
    public string CorrectAnswer { get; set; }
}
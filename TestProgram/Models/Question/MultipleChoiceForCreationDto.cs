namespace TestProgram.Models.Question;

public class MultipleChoiceForCreationDto : QuestionForCreationDto
{
    public List<string> Options { get; set; }
    public string CorrectAnswer { get; set; }
}
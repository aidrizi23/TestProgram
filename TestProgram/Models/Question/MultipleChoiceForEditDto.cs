namespace TestProgram.Models.Question;


public class MultipleChoiceForEditDto : QuestionForEditDto
{
    public List<string> Options { get; set; } = new List<string>();
    public string CorrectAnswer { get; set; }
}
namespace TestProgram.Data;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; set; }
    public string CorrectAnswer { get; set; }

    public override bool ValidateAnswer(object studentAnswer)
    {
        return studentAnswer == CorrectAnswer;
    }
    
}
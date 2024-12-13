namespace TestProgram.Data;

public class TextQuestion : Question
{
    public string ExceptedAnswer { get; set; }
    public override bool ValidateAnswer(object studentAnswer)
    {
        return studentAnswer.ToString() == ExceptedAnswer;
    }
}
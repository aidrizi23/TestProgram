namespace TestProgram.Data;

public class TextQuestion : Question
{
    public List<string> ExpectedAnswer { get; set; } = new List<string>();
    
    public override bool ValidateAnswer(object studentAnswer)
    {
        return ExpectedAnswer.Contains(studentAnswer);
    }
}
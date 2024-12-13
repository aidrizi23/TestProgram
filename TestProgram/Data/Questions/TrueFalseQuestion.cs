namespace TestProgram.Data;

public class TrueFalseQuestion : Question
{
    public bool CorrectAnswer { get; set; }
    
    public override bool ValidateAnswer(object studentAnswer)
    {
        return studentAnswer.ToString() == CorrectAnswer.ToString();
    }
}
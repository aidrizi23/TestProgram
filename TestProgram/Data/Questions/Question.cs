using TestProgram.Data.Enums;

namespace TestProgram.Data;

public abstract class Question : BaseEntity
{
    public string QuestionText { get; set; }
    public int Points { get; set; }
    public QuestionType Type { get; set; }
    
    // relationships
    public int TestId { get; set; }
    public virtual Test Test { get; set; }
    public abstract bool ValidateAnswer(object studentAnswer);
}


namespace TestProgram.Data;

public class Test : BaseEntity
{
    // will use only for the relationship with the question class
    public string TestName { get; set; }
    
    public virtual ICollection<Question> Questions { get; set; }
    
    public string TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }
    
}
namespace TestProgram.Data;

public class Test : BaseEntity
{
    
    public string TestName { get; set; }
    
    public virtual ICollection<Question> Questions { get; set; }
    
    public string TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }
    
   
    public int TotalPoints { get; set; } = 0;
    
    public bool IsLocked { get; set; } = false;
    
    public int CalculateTotalPoints()
    {
        int total = 0;
        foreach (var question in Questions)
        {
            total += question.Points;
        }
        return total;
    }
   
}
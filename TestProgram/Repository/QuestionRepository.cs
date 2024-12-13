using Microsoft.EntityFrameworkCore;
using TestProgram.Data;

namespace TestProgram.Repository;

public class QuestionRepository : IQuestionRepository
{
    private readonly ApplicationDbContext _context;
    public QuestionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<IEnumerable<Question?>> GetQuestionsByTestId(int testId)
    {
        return await _context.Questions.Where(q => q.TestId == testId).ToListAsync();
    }
    
    
    public async Task<Question?> GetQuestionById(int questionId)
    {
        return await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
    }
    
   
}

public interface IQuestionRepository
{
    Task<IEnumerable<Question?>> GetQuestionsByTestId(int testId);
    Task<Question?> GetQuestionById(int questionId);
}

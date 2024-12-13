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
    
    
    // method to get the qustions by the id of the test
    public async Task<IEnumerable<Question?>> GetQuestionsByTestId(int testId)
    {
        return await _context.Questions.Where(q => q.TestId == testId).ToListAsync();
    }
    
    // method to get a question by it's id
    public async Task<Question?> GetQuestionById(int questionId)
    {
        return await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
    }
    
    // method to add the question to the database
    public async Task Add(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(Question question)
    {
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
    }
    
    public async Task Delete(Question question)
    {
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteById(int questionId)
    {
        var question = await GetQuestionById(questionId);
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
    
}

public interface IQuestionRepository
{
    Task<IEnumerable<Question?>> GetQuestionsByTestId(int testId);
    Task<Question?> GetQuestionById(int questionId);
    Task Add(Question question);
    Task Update(Question question);
    Task Delete(Question question);
    Task DeleteById(int questionId);
}

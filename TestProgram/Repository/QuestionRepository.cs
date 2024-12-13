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
    
    // this method will create a question based on the type of question 
    // and add it to the database
    public async Task<Question> CreateQuestion(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return question;
    }   
        
    // this method will update a question based on the type of question
    // and update it in the database
    public async Task<Question> UpdateQuestion(Question question)
    {
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
        return question;
    }
    
    // this method will delete a question based on the type of question
    // and delete it from the database
    public async Task DeleteQuestion(Question question)
    {
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
    }
    
   
}

public interface IQuestionRepository
{
    Task<IEnumerable<Question?>> GetQuestionsByTestId(int testId);
    Task<Question?> GetQuestionById(int questionId);
}

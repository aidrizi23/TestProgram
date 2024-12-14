using Microsoft.EntityFrameworkCore;
using TestProgram.Data;

namespace TestProgram.Repository;

public class TestRepository : ITestRepository
{
    private readonly ApplicationDbContext _context;
    public TestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Test?>> GetTestsByTeacherId(string teacherId)
    {
        return await _context.Tests?.Where(t => t.TeacherId == teacherId).ToListAsync();
    }
    
    public async Task<Test?> GetTestById(int testId)
    {
        return await _context.Tests.FirstOrDefaultAsync(t => t.Id == testId);
    }
    
    public async Task<Test> CreateTest(Test test)
    {
        _context.Tests.Add(test);
        await _context.SaveChangesAsync();
        return test;
    }
    
    public async Task<Test> UpdateTest(Test test)
    {
        _context.Tests.Update(test);
        await _context.SaveChangesAsync();
        return test;
    }
    
    public async Task DeleteTest(Test test)
    {
        _context.Tests.Remove(test);
        await _context.SaveChangesAsync();
    }
    
}

public interface ITestRepository
{
    Task<IEnumerable<Test?>> GetTestsByTeacherId(string teacherId);
    Task<Test?> GetTestById(int testId);
    Task<Test> CreateTest(Test test);
    Task<Test> UpdateTest(Test test);
    Task DeleteTest(Test test);
}


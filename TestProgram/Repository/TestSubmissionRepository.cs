using Microsoft.EntityFrameworkCore;
using TestProgram.Data;

namespace TestProgram.Repository;

public class TestSubmissionRepository : ITestSubmissionRepository
{
    private readonly ApplicationDbContext _context;

    public TestSubmissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TestSubmission> CreateSubmission(TestSubmission submission)
    {
        _context.TestSubmissions.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    public async Task<TestSubmission?> GetSubmissionById(int id)
    {
        return await _context.TestSubmissions
            .Include(ts => ts.Answers)
            .ThenInclude(sa => sa.Question)
            .FirstOrDefaultAsync(ts => ts.Id == id);
    }

    public async Task<IEnumerable<TestSubmission>> GetSubmissionsByTestId(int testId)
    {
        return await _context.TestSubmissions
            .Where(ts => ts.TestId == testId)
            .Include(ts => ts.Answers)
            .ThenInclude(sa => sa.Question)
            .ToListAsync();
    }

    public async Task<TestSubmission> UpdateSubmission(TestSubmission submission)
    {
        _context.TestSubmissions.Update(submission);
        await _context.SaveChangesAsync();
        return submission;
    }
}

public interface ITestSubmissionRepository
{
    Task<TestSubmission> CreateSubmission(TestSubmission submission);
    Task<TestSubmission?> GetSubmissionById(int id);
    Task<IEnumerable<TestSubmission>> GetSubmissionsByTestId(int testId);
    Task<TestSubmission> UpdateSubmission(TestSubmission submission);
}
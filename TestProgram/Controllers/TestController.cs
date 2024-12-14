using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProgram.Data;
using TestProgram.Repository;

namespace TestProgram.Controllers;

public class TestController : Controller
{
    private readonly ITestRepository _testRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly UserManager<Teacher> _userManager;
    
    public TestController(ITestRepository testRepository, IQuestionRepository questionRepository, UserManager<Teacher> userManager)
    {
        _testRepository = testRepository;
        _questionRepository = questionRepository;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var tests = await _testRepository.GetTestsByTeacherId(user.Id);
        return View(tests);
    }

   
    
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProgram.Data;
using TestProgram.Models.Test;
using TestProgram.Repository;

namespace TestProgram.Controllers;

[Route("tests")]
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
    
    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var tests = await _testRepository.GetTestsByTeacherId(user.Id);
        return View(tests);
    }
    
    [Authorize]
    [HttpGet("create")]
    public IActionResult Create()
    {
        var dto = new TestForCreationDto();
        return View();
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create(TestForCreationDto dto)
    {
        
        // get the current user that is logged in
        var user = await _userManager.GetUserAsync(User);
        if (user is not Teacher)
        {
            return Unauthorized();
        }

        var test = new Test()
        {
            TestName = dto.TestName,
            TeacherId = user.Id,
        };
        
        await _testRepository.CreateTest(test);
        
        return RedirectToAction("Index");
    }
    
    // details method for the test
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var test = await _testRepository.GetTestById(id);
        return View(test);
    }
    
    
    [Authorize]
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        Test? test = await _testRepository.GetTestById(id);

        if (test == null)
        {
            return NotFound();
        }

        if (user is not Teacher || test.TeacherId != user.Id)
        {
            return Unauthorized();
        }

        await _testRepository.DeleteTest(test);
        return RedirectToAction("Index");
    }


   
    
}
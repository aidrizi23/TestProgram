using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProgram.Data;
using TestProgram.Data.Enums;
using TestProgram.Models.Question;
using TestProgram.Repository;

namespace TestProgram.Controllers;

[Route("questions")]
public class QuestionController : Controller
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ITestRepository _testRepository;
    private readonly UserManager<Teacher> _userManager;
    
    public QuestionController(IQuestionRepository questionRepository, ITestRepository testRepository, UserManager<Teacher> userManager)
    {
        _questionRepository = questionRepository;
        _testRepository = testRepository;
        _userManager = userManager;
    }
    
    // will have a create controller method for each type of question
    [Authorize]
    [HttpGet("tf")]
    public async Task<IActionResult> CreateTrueFalseQuestion(int testId)
    {
        var dto = new TrueFalseQuestion();
        dto.TestId = testId;
        return View(dto);
    }
    
    [Authorize]
    [HttpPost("tf")]
    public async Task<IActionResult> CreateTrueFalseQuestion(TrueFalseQuestion dto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is not Teacher)
        {
            return Unauthorized();
        }
        
        var test = await _testRepository.GetTestById(dto.TestId);
        if (test is null)
        {
            return NotFound();
        }
        
        var question = new TrueFalseQuestion()
        {
            QuestionText = dto.QuestionText,
            Points = dto.Points,
            Type = QuestionType.TrueFalse,
            TestId = dto.TestId,
            CorrectAnswer = dto.CorrectAnswer,
        };
        
        await _questionRepository.CreateQuestion(question);
        
        return RedirectToAction("Details", "Test", new { id = dto.TestId });
    }


    [Authorize]
    [HttpGet("mc")]
    public async Task<IActionResult> CreateMultipleChoiceQuestion(int testId)
    {
        var dto = new MultipleChoiceForCreationDto();
        dto.TestId = testId;
        return View(dto);
    }

    [Authorize]
    [HttpPost("mc")]
    public async Task<IActionResult> CreateMultipleChoiceQuestion(MultipleChoiceForCreationDto dto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is not Teacher)
        {
            return Unauthorized();
        }
        
        var test = await _testRepository.GetTestById(dto.TestId);
        if (test is null)
        {
            return NotFound();
        }
        
        var question = new MultipleChoiceQuestion()
        {
            QuestionText = dto.QuestionText,
            Points = dto.Points,
            Type = QuestionType.MultipleChoice,
            TestId = dto.TestId,
            CorrectAnswer = dto.CorrectAnswer,
            Options = dto.Options,
        };
        
        await _questionRepository.CreateQuestion(question);
        
        return RedirectToAction("Details", "Test", new { id = dto.TestId });
    }
}
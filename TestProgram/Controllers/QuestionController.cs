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
    
    
    [Authorize]
    [HttpGet("text")]
    public async Task<IActionResult> CreateTextQuestion(int testId)
    {
        var dto = new TextQuestionForCreationDto();
        dto.TestId = testId;
        return View(dto);
    }
    
    [Authorize]
    [HttpPost("text")]
    public async Task<IActionResult> CreateTextQuestion(TextQuestion dto)
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
        
        var question = new TextQuestion()
        {
            QuestionText = dto.QuestionText,
            Points = dto.Points,
            Type = QuestionType.Text,
            TestId = dto.TestId,
            ExpectedAnswer = dto.ExpectedAnswer
        };
        
        await _questionRepository.CreateQuestion(question);
        
        return RedirectToAction("Details", "Test", new { id = dto.TestId });
    }
    
    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized();
        }
        
        var question = await _questionRepository.GetQuestionById(id);
        if (question is null)
        {
            return NotFound();
        }
        
        await _questionRepository.DeleteQuestion(question);
        
        return RedirectToAction("Details", "Test", new { id = question.TestId });
    }
}
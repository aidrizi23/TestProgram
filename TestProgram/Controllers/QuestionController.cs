using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProgram.Data;
using TestProgram.Data.Enums;
using TestProgram.Models.Question;
using TestProgram.Repository;

namespace TestProgram.Controllers;

[Route("[controller]")]
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
        
        var test = await _testRepository.GetTestByIdWithQuestions(dto.TestId);
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
        
        // updating the toatal points of a test
        test.TotalPoints = test.CalculateTotalPoints();
        await _testRepository.UpdateTest(test);

        
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
        
        var test = await _testRepository.GetTestByIdWithQuestions(dto.TestId);
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
        
        // updating the toatal points of a test
        test.TotalPoints = test.CalculateTotalPoints();
        await _testRepository.UpdateTest(test);

        
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
        
        var test = await _testRepository.GetTestByIdWithQuestions(dto.TestId);
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
        
        // updating the toatal points of a test
        test.TotalPoints = test.CalculateTotalPoints();
        await _testRepository.UpdateTest(test);
        

        
        return RedirectToAction("Details", "Test", new { id = dto.TestId });
    }
    
    [Authorize]
    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized();
        }
        
        
        var question = await _questionRepository.GetQuestionById(id);
        
        
        var test = await _testRepository.GetTestByIdWithQuestions(question!.TestId);
        if (question is null)
        {
            return NotFound();
        }
        
        await _questionRepository.DeleteQuestion(question);
        
        // updating the toatal points of a test
        test.TotalPoints = test.CalculateTotalPoints();
        await _testRepository.UpdateTest(test);

        
        return RedirectToAction("Details", "Test", new { id = question.TestId });
    }

    [Authorize]
    [HttpGet("edittf/{id:int}")]
    public async Task<IActionResult> Edit(int id)
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
        
        if (question is TrueFalseQuestion)
        {
            var dto = new TrueFalseForEditDto()
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                Points = question.Points,
                TestId = question.TestId,
                CorrectAnswer = ((TrueFalseQuestion) question).CorrectAnswer
            };
            return View(dto);
        }
      
        else
        {
            return NotFound();
        }
        
    }
    
    [Authorize]
    [HttpPost("edittf/{id:int}")]
    public async Task<IActionResult> Edit(TrueFalseForEditDto dto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized();
        }
        
        var question = await _questionRepository.GetQuestionById(dto.Id);
        if (question is null)
        {
            return NotFound();
        }
        
        if (question is TrueFalseQuestion)
        {
            question.QuestionText = dto.QuestionText;
            question.Points = dto.Points;
            ((TrueFalseQuestion) question).CorrectAnswer = dto.CorrectAnswer;
            await _questionRepository.UpdateQuestion(question);
            return RedirectToAction("Details", "Test", new { id = question.TestId });
        }
        
        else
        {
            return NotFound();
        }
    }
    
    [Authorize]
    [HttpGet("editmc/{id:int}")]
    public async Task<IActionResult> EditMultipleChoice(int id)
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
        
        if (question is MultipleChoiceQuestion)
        {
            var dto = new MultipleChoiceForEditDto()
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                Points = question.Points,
                TestId = question.TestId,
                CorrectAnswer = ((MultipleChoiceQuestion) question).CorrectAnswer,
                Options = ((MultipleChoiceQuestion) question).Options
            };
            return View(dto);
        }
      
        else
        {
            return NotFound();
        }
        
    }
    
    [Authorize]
    [HttpPost("editmc/{id:int}")]
    public async Task<IActionResult> EditMultipleChoice(MultipleChoiceForEditDto dto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized();
        }
        
        var question = await _questionRepository.GetQuestionById(dto.Id);
        if (question is null)
        {
            return NotFound();
        }
        
        if (question is MultipleChoiceQuestion)
        {
            question.QuestionText = dto.QuestionText;
            question.Points = dto.Points;
            ((MultipleChoiceQuestion) question).CorrectAnswer = dto.CorrectAnswer;
            ((MultipleChoiceQuestion) question).Options = dto.Options;
            await _questionRepository.UpdateQuestion(question);
            return RedirectToAction("Details", "Test", new { id = question.TestId });
        }
        
        else
        {
            return NotFound();
        }
    }
    
    [Authorize]
    [HttpGet("edittext/{id:int}")]
    public async Task<IActionResult> EditText(int id)
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
        
        if (question is TextQuestion)
        {
            var dto = new TextQuestionForEditDto()
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                Points = question.Points,
                TestId = question.TestId,
                ExpectedAnswer = ((TextQuestion) question).ExpectedAnswer
            };
            return View(dto);
        }
      
        else
        {
            return NotFound();
        }
        
    }
    
    [Authorize]
    [HttpPost("edittext/{id:int}")]
    public async Task<IActionResult> EditText(TextQuestionForEditDto dto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized();
        }
        
        var question = await _questionRepository.GetQuestionById(dto.Id);
        if (question is null)
        {
            return NotFound();
        }
        
        if (question is TextQuestion)
        {
            question.QuestionText = dto.QuestionText;
            question.Points = dto.Points;
            ((TextQuestion) question).ExpectedAnswer = dto.ExpectedAnswer;
            await _questionRepository.UpdateQuestion(question);
            return RedirectToAction("Details", "Test", new { id = question.TestId });
        }
        
        else
        {
            return NotFound();
        }
    }
}
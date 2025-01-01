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
    private readonly ITestSubmissionRepository _testSubmissionRepository;
    
    public TestController(ITestRepository testRepository, ITestSubmissionRepository  testSubmissionRepository, IQuestionRepository questionRepository, UserManager<Teacher> userManager)
    {
        _testRepository = testRepository;
        _questionRepository = questionRepository;
        _userManager = userManager;
        _testSubmissionRepository = testSubmissionRepository;
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
        
        return RedirectToAction("Details", new { id = test.Id }); // will return to the details method 
        // so that i can add a button there to create new questions for the test
        
    }
    
    // details method for the test
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var test = await _testRepository.GetTestByIdWithQuestions(id);
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
    
    // method to generate a link to this test for other students to access
    [Authorize]
    [HttpGet("generate-link/{id}")]
    public async Task<IActionResult> GenerateLink(int id)
    {
       // get the current user (if it is the teacher or not)
       var teacher =  await _userManager.GetUserAsync(User);
         if (teacher is not Teacher)
         {
              return Unauthorized();
         }
         
            // get the test by id
        var test = await _testRepository.GetTestById(id);
        if (test == null)
        {
            return NotFound();
        }
        
        // generate a link for the test
        var testLink = Url.Action("StartTest", "StudentTest", new { testId = test.Id }, Request.Scheme);
        return View("TestLink", testLink);
         
    }
    
  [HttpGet("{id}/submissions")]
        public async Task<IActionResult> ViewSubmissions(int id)
        {
            var test = await _testRepository.GetTestById(id);
            if (test == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user.Id != test.TeacherId)
            {
                return Unauthorized();
            }

            var submissions = await _testSubmissionRepository.GetSubmissionsByTestId(id);
            var model = new TestSubmissionsViewModel
            {
                TestId = id,
                TestName = test.TestName,
                Submissions = submissions.Select(s => new TestSubmissionViewModel
                {
                    Id = s.Id,
                    TestId = s.TestId,
                    TestName = test.TestName,
                    StudentFullName = $"{s.StudentFirstName} {s.StudentLastName}",
                    SubmissionTime = s.SubmissionTime,
                    TotalScore = s.TotalScore
                }).ToList()
            };

            return View(model);
        }

        [HttpGet("submission/{id}")]
        public async Task<IActionResult> ReviewSubmission(int id)
        {
            var submission = await _testSubmissionRepository.GetSubmissionById(id);
            if (submission == null)
            {
                return NotFound();
            }

            var test = await _testRepository.GetTestById(submission.TestId);
            if (test == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user.Id != test.TeacherId)
            {
                return Unauthorized();
            }

            var model = new TestSubmissionViewModel
            {
                Id = submission.Id,
                TestId = submission.TestId,
                TestName = test.TestName,
                StudentFullName = $"{submission.StudentFirstName} {submission.StudentLastName}",
                SubmissionTime = submission.SubmissionTime,
                TotalScore = submission.TotalScore,
                Answers = submission.Answers.Select(a => new StudentAnswerViewModel
                {
                    QuestionId = a.QuestionId,
                    QuestionText = a.Question.QuestionText,
                    StudentAnswer = a.Answer,
                    Score = a.Score,
                    MaxScore = a.Question.Points
                }).ToList()
            };

            return View(model);
        }

        [HttpPost("submission/{id}")]
        public async Task<IActionResult> UpdateSubmissionScores(int id, [FromBody] List<StudentAnswerViewModel> answers)
        {
            var submission = await _testSubmissionRepository.GetSubmissionById(id);
            if (submission == null)
            {
                return NotFound();
            }

            var test = await _testRepository.GetTestById(submission.TestId);
            if (test == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user.Id != test.TeacherId)
            {
                return Unauthorized();
            }

            foreach (var answer in answers)
            {
                var submissionAnswer = submission.Answers.FirstOrDefault(a => a.QuestionId == answer.QuestionId);
                if (submissionAnswer != null)
                {
                    submissionAnswer.Score = answer.Score;
                }
            }

            submission.TotalScore = submission.Answers.Sum(a => a.Score);
            await _testSubmissionRepository.UpdateSubmission(submission);

            return Ok(new { TotalScore = submission.TotalScore });
        }

        [Authorize]
        [HttpPost("lock/{id}")]
        public async Task<IActionResult> LockTest(int id)
        {
            var test = await _testRepository.GetTestById(id);
            if (test == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user.Id != test.TeacherId)
            {
                return Unauthorized();
            }

            test.IsLocked = !test.IsLocked;
            await _testRepository.UpdateTest(test);

            return Json(new { isLocked = test.IsLocked });
        }

    
}
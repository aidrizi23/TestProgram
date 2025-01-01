using Microsoft.AspNetCore.Mvc;
using TestProgram.Data;
using TestProgram.Models.Student;
using TestProgram.Models.Test;
using TestProgram.Models.Test;
using TestProgram.Repository;

namespace TestProgram.Controllers;

[Route("student-test")]
public class StudentTestController : Controller
{
    private readonly ITestRepository _testRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly ITestSubmissionRepository _testSubmissionRepository;
    public StudentTestController(ITestRepository testRepository,ITestSubmissionRepository testSubmissionRepository, IQuestionRepository questionRepository)
    {
        _testRepository = testRepository;
        _questionRepository = questionRepository;
        _testSubmissionRepository = testSubmissionRepository;
    }

    [HttpGet("{testId}")]
    public async Task<IActionResult> StartTest(int testId)
    {
        var test = await _testRepository.GetTestByIdWithQuestions(testId);
        if (test == null || test.IsLocked)
        {
            return NotFound("The test you're looking for is either locked or doesn't exist.");
        }
        

        var model = new StartTestViewModel
        {
            TestId = test.Id,
            TestName = test.TestName
        };

        return View(model);
    }

    [HttpPost("{testId}")]
    public async Task<IActionResult> StartTest(int testId, StartTestViewModel model)
    {
     

        var test = await _testRepository.GetTestByIdWithQuestions(testId);
        if (test == null)
        {
            return NotFound();
        }

        // Here you might want to save the student information or create a session

        return RedirectToAction("TakeTest", new { testId = test.Id, firstName = model.FirstName ?? string.Empty, lastName = model.LastName ?? string.Empty });


    }

//     [HttpGet("{testId}/take")]
//     public async Task<IActionResult> TakeTest(int testId, string firstName, string lastName,
//         int currentQuestionIndex = 0)
//     {
//         var test = await _testRepository.GetTestByIdWithQuestions(testId);
//         if (test == null || test.IsLocked)
//         {
//             return NotFound();
//         }
//
//         var questions = test.Questions.ToList();
//
//         var model = new TakeTestViewModel
//         {
//             TestId = test.Id,
//             TestName = test.TestName,
//             Questions = questions,
//             FirstName = firstName,
//             LastName = lastName,
//             CurrentQuestionIndex = currentQuestionIndex,
//             TotalQuestions = questions.Count
//         };
//
//         return View(model);
//     }
//
//     // [HttpPost("{testId}/submit")]
//     // public async Task<IActionResult> SubmitTest(int testId, TakeTestViewModel model)
//     // {
//     //     var test = await _testRepository.GetTestByIdWithQuestions(testId);
//     //     if (test == null || test.IsLocked)
//     //     {
//     //         return NotFound();
//     //     }
//     //
//     //     var submission = new TestSubmission
//     //     {
//     //         TestId = testId,
//     //         StudentFirstName = model.FirstName,
//     //         StudentLastName = model.LastName,
//     //         SubmissionTime = DateTime.UtcNow,
//     //         Answers = model.Answers.Select(a => new StudentAnswer
//     //         {
//     //             QuestionId = a.Key,
//     //             Answer = a.Value
//     //         }).ToList()
//     //     };
//     //
//     //     AutoGradeSubmission(submission, test.Questions);
//     //
//     //     await _testSubmissionRepository.CreateSubmission(submission);
//     //
//     //     var submissionViewModel = new TestSubmissionViewModel
//     //     {
//     //         Id = submission.Id,
//     //         TestId = submission.TestId,
//     //         TestName = test.TestName,
//     //         StudentFullName = $"{submission.StudentFirstName} {submission.StudentLastName}",
//     //         SubmissionTime = submission.SubmissionTime,
//     //         TotalScore = submission.TotalScore,
//     //         Answers = submission.Answers.Select(a => new StudentAnswerViewModel
//     //         {
//     //             QuestionId = a.QuestionId,
//     //             QuestionText = test.Questions.First(q => q.Id == a.QuestionId).QuestionText,
//     //             StudentAnswer = a.Answer,
//     //             Score = a.Score,
//     //             MaxScore = test.Questions.First(q => q.Id == a.QuestionId).Points
//     //         }).ToList()
//     //     };
//     //     submission.StudentLastName = model.LastName;
//     //     submission.StudentFirstName = model.FirstName;
//     //
//     //     return View("ThankYou", submissionViewModel);
//     // }
//     
//     
//     [HttpPost("{testId}/submit")]
//     public async Task<IActionResult> SubmitTest(int testId, TakeTestViewModel model)
//     {
//         var test = await _testRepository.GetTestByIdWithQuestions(testId);
//         if (test == null)
//         {
//             return NotFound();
//         }
//
//         var submission = new TestSubmission
//         {
//             TestId = testId,
//             StudentFirstName = model.FirstName,
//             StudentLastName = model.LastName,
//             SubmissionTime = DateTime.UtcNow,
//             Answers = new List<StudentAnswer>()
//         };
//
//         foreach (var question in test.Questions)
//         {
//             submission.Answers.Add(new StudentAnswer
//             {
//                 QuestionId = question.Id,
//                 Answer = model.Answers.TryGetValue(question.Id, out var answer) ? answer : string.Empty
//             });
//         }
//
//         AutoGradeSubmission(submission, test.Questions);
//
//         await _testSubmissionRepository.CreateSubmission(submission);
//     var submissionViewModel = new TestSubmissionViewModel
//     {
//         Id = submission.Id,
//         TestId = submission.TestId,
//         TestName = test.TestName,
//         StudentFullName = $"{submission.StudentFirstName} {submission.StudentLastName}",
//         SubmissionTime = submission.SubmissionTime,
//         TotalScore = submission.TotalScore,
//         Answers = submission.Answers.Select(a => new StudentAnswerViewModel
//         {
//             QuestionId = a.QuestionId,
//             QuestionText = test.Questions.First(q => q.Id == a.QuestionId).QuestionText,
//             StudentAnswer = a.Answer,
//             Score = a.Score,
//             MaxScore = test.Questions.First(q => q.Id == a.QuestionId).Points
//         }).ToList()
//     };
//
//     return View("ThankYou", submissionViewModel);
// }
//
//
//
//
//
//     private void AutoGradeSubmission(TestSubmission submission, IEnumerable<Question> questions)
//     {
//         float totalScore = 0;
//
//         foreach (var answer in submission.Answers)
//         {
//             var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
//             if (question == null) continue;
//
//             switch (question)
//             {
//                 case TrueFalseQuestion tf:
//                     answer.Score = !string.IsNullOrWhiteSpace(answer.Answer) && 
//                                    answer.Answer.Equals(tf.CorrectAnswer.ToString(), StringComparison.OrdinalIgnoreCase) 
//                         ? tf.Points : 0;
//                     break;
//                 case MultipleChoiceQuestion mc:
//                     answer.Score = !string.IsNullOrWhiteSpace(answer.Answer) && 
//                                    answer.Answer.Equals(mc.CorrectAnswer, StringComparison.OrdinalIgnoreCase) 
//                         ? mc.Points : 0;
//                     break;
//                 case TextQuestion tq:
//                     // For text questions, we'll need a more sophisticated comparison
//                     // This is a simple example that checks if the answer contains any of the expected answers
//                     answer.Score = !string.IsNullOrWhiteSpace(answer.Answer) && 
//                                    tq.ExpectedAnswer.Any(ea => answer.Answer.Contains(ea, StringComparison.OrdinalIgnoreCase)) 
//                         ? tq.Points : 0;
//                     break;
//             }
//
//             totalScore += answer.Score;
//         }
//
//         submission.TotalScore = totalScore;
//     }


[HttpGet("{testId}/take")]
public async Task<IActionResult> TakeTest(
    int testId, 
    string firstName, 
    string lastName, 
    int currentQuestionIndex = 0, 
    [FromQuery]Dictionary<int, string> answers = null)
    {
        var test = await _testRepository.GetTestByIdWithQuestions(testId);
        if (test == null)
        {
            return NotFound();
        }

        var questions = test.Questions.ToList();

        var model = new TakeTestViewModel
        {
            TestId = test.Id,
            TestName = test.TestName,
            Questions = questions,
            FirstName = firstName,
            LastName = lastName,
            CurrentQuestionIndex = currentQuestionIndex,
            TotalQuestions = questions.Count,
            Answers = answers ?? new Dictionary<int, string>()
        };

        return View(model);
    }

    [HttpPost("{testId}/submit")]
    public async Task<IActionResult> SubmitTest(int testId, TakeTestViewModel model)
    {
        var test = await _testRepository.GetTestByIdWithQuestions(testId);
        if (test == null)
        {
            return NotFound();
        }

        var submission = new TestSubmission
        {
            TestId = testId,
            StudentFirstName = model.FirstName,
            StudentLastName = model.LastName,
            SubmissionTime = DateTime.UtcNow,
            Answers = new List<StudentAnswer>()
        };

        foreach (var question in test.Questions)
        {
            var answer = model.Answers.TryGetValue(question.Id, out var value) ? value : string.Empty;
            submission.Answers.Add(new StudentAnswer
            {
                QuestionId = question.Id,
                Answer = answer
            });
        }

        AutoGradeSubmission(submission, test.Questions);

        await _testSubmissionRepository.CreateSubmission(submission);

        var submissionViewModel = new TestSubmissionViewModel
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
                QuestionText = test.Questions.First(q => q.Id == a.QuestionId).QuestionText,
                StudentAnswer = a.Answer,
                Score = a.Score,
                MaxScore = test.Questions.First(q => q.Id == a.QuestionId).Points
            }).ToList()
        };

        return View("ThankYou", submissionViewModel);
    }

    private void AutoGradeSubmission(TestSubmission submission, IEnumerable<Question> questions)
    {
        float totalScore = 0;

        foreach (var answer in submission.Answers)
        {
            var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
            if (question == null) continue;

            switch (question)
            {
                case TrueFalseQuestion tf:
                    answer.Score = !string.IsNullOrWhiteSpace(answer.Answer) && 
                                   answer.Answer.Equals(tf.CorrectAnswer.ToString(), StringComparison.OrdinalIgnoreCase) 
                                   ? tf.Points : 0;
                    break;
                case MultipleChoiceQuestion mc:
                    answer.Score = !string.IsNullOrWhiteSpace(answer.Answer) && 
                                   answer.Answer.Equals(mc.CorrectAnswer, StringComparison.OrdinalIgnoreCase) 
                                   ? mc.Points : 0;
                    break;
                case TextQuestion tq:
                    answer.Score = !string.IsNullOrWhiteSpace(answer.Answer) && 
                                   tq.ExpectedAnswer.Any(ea => answer.Answer.Contains(ea, StringComparison.OrdinalIgnoreCase)) 
                                   ? tq.Points : 0;
                    break;
            }

            totalScore += answer.Score;
        }

        submission.TotalScore = totalScore;
    }
}

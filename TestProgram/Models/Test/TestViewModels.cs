namespace TestProgram.Models.Test
{
    public class TestSubmissionsViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public List<TestSubmissionViewModel> Submissions { get; set; }
    }

    public class TestSubmissionViewModel
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string StudentFullName { get; set; }
        public DateTime SubmissionTime { get; set; }
        public float TotalScore { get; set; }
        public List<StudentAnswerViewModel> Answers { get; set; }
    }

    public class StudentAnswerViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string StudentAnswer { get; set; }
        public float Score { get; set; }
        public float MaxScore { get; set; }
    }
}
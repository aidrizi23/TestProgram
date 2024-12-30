using System.ComponentModel.DataAnnotations;

namespace TestProgram.Models.Student
{
    public class StartTestViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }

    public class TakeTestViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Data.Question> Questions { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
        public int CurrentQuestionIndex { get; set; } // New property for pagination
        public int TotalQuestions { get; set; } // New property for pagination
    }
}
using EnglishApp.Models;

namespace EnglishApp.Dto
{
    public class QuestionDto
    {
        public string? Text { get; set; }
        public string? CorrectOption { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
    }
}

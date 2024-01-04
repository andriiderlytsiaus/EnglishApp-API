using EnglishApp.Models;

namespace EnglishApp.Dto
{
    public class QuizDto
    {
        public string Type { get; set; } = null!;
        public ICollection<QuestionDto> Questions { get; set; } = null!;
        public int LessonId { get; set; }
    }
}

namespace EnglishApp.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = null!;
        public Lesson Lesson { get; set; } = null!;
        public int LessonId { get; set; } 

        
    }
}

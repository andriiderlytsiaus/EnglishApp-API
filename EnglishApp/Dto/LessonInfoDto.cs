namespace EnglishApp.Dto
{
    public class LessonInfoDto
    {
        public int LessonId { get; set; }
        public string? Keywords { get; set;}
        public string Difficulty { get; set; } = null!;
    }
}

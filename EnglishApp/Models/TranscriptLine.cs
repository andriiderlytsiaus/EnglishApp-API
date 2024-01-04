using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishApp.Models
{
    public class TranscriptLine
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public float StartTime { get; set; }
        public int LessonId { get; set; }
        public float? Duration { get; set; }
        public Lesson Lesson { get; set; } = null!;
    }
}

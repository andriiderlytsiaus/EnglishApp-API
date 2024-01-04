using System.Data;

namespace EnglishApp.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Goal { get; set; }
        public string? Theory { get; set; }
        public int Rating { get; set; }
        public string YouTubeVideoId { get; set; } = null!;
        public string? Keywords { get; set; }
        public string? Difficulty { get; set; }
        public DateTime? CreationTime { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; } = null!;
        public ICollection<TranscriptLine> TranscriptLines { get; set;} = null!;
        public ICollection<Quiz>? Quizzes { get; set; }
    }
}

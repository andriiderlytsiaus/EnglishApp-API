using EnglishApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Data
{
    public class EnglishAppContext :DbContext
    {
        public EnglishAppContext(DbContextOptions options) :base(options) { }
        public DbSet<WordDefinition> WordDefinitions { get; set; } = null!;
        public DbSet<WordMeaning> WordMeanings { get; set; } = null!;
        public DbSet<Word> Words { get; set; } = null!;
        public DbSet<SavedWord> SavedWords { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<TranscriptLine> TranscriptLines { get; set; } = null!;
        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;

    }
}

using EnglishApp.Data;
using EnglishApp.Interface;
using EnglishApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventSource;

namespace EnglishApp.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly EnglishAppContext _context;
        public LessonRepository(EnglishAppContext context)
        {
            _context = context;
        }

        public bool AddRating(int lessonId, string rating)
        {
            var lesson = GetLesson(lessonId);
            if(rating == "up")
                lesson.Rating++;
            else /*if (rating == "down")*/
                lesson.Rating--;
            _context.Lessons.Update(lesson);
            return Save();
        }

        public bool AddTheory(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            return Save();
        }

        public bool CreateLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            return Save();
        }

        public bool DeleteLesson(Lesson lesson)
        {
            _context.Lessons.Remove(lesson);
            return Save();
        }

        public Lesson GetLesson(int id)
        {
            Lesson lesson = _context.Lessons
                .Include(u => u.TranscriptLines)
            .Include(y => y.Quizzes)
                .ThenInclude(q => q.Questions)
                .Include(q =>q.User)
            .FirstOrDefault(x => x.Id == id);   

            return lesson;
        }

        public ICollection<Lesson> GetLessons()
        {
            return _context.Lessons.ToList();
        }

        public ICollection<Lesson> GetLessonsByUser(int userId)
        {
            return _context.Lessons.Where(x => x.UserId == userId).ToList();
        }

        public ICollection<Lesson> GetTopRatedLessons()
        {
            return GetLessons().OrderByDescending(x => x.Rating).Take(7).ToList();
        }

        public bool LessonExists(int id)
        {
            if (_context.Lessons.Any(x => x.Id == id))
                return true;
            else return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

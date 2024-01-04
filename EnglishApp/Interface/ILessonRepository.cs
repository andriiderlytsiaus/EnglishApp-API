using EnglishApp.Models;

namespace EnglishApp.Interface
{
    public interface ILessonRepository
    {
        ICollection<Lesson> GetLessons();
        ICollection<Lesson> GetTopRatedLessons();
        ICollection<Lesson> GetLessonsByUser(int userId);
        Lesson GetLesson(int id);
        bool CreateLesson(Lesson lesson);
        bool AddRating(int lessonId, string rating);
        bool AddTheory(Lesson lesson);
        bool LessonExists(int id);
        bool DeleteLesson (Lesson lesson);
        bool Save();
    }
}

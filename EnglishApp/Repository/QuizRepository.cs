using EnglishApp.Data;
using EnglishApp.Interface;
using EnglishApp.Models;

namespace EnglishApp.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly EnglishAppContext _context;
        public QuizRepository(EnglishAppContext context)
        {
            _context = context;
        }
        public bool AddQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            return Save();
            
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

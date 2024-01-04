using EnglishApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Interface
{
    public interface IQuizRepository
    {
        bool AddQuiz(Quiz quiz);

    }
}

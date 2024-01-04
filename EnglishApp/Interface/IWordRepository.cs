using EnglishApp.Dto;
using EnglishApp.Models;

namespace EnglishApp.Interface
{
    public interface IWordRepository
    {
        Word GetWord(int id);
        Word GetWord(string word);
        Task<bool> CreateWord(string word);
        bool WordExists(int Id);
        bool WordExists(string word);
        bool Save();
    }
}

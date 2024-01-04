using EnglishApp.Models;

namespace EnglishApp.Interface
{
    public interface ISavedWordRepository
    {
        SavedWord GetSavedWord(int id);
        ICollection<SavedWord> GetSavedWordsByUser(int userId);
        ICollection<WordMeaning> GetSavedWordDetails(string word);
        ICollection<Word> GetWordsByUser(int userId);
        bool CreateSavedWord(SavedWord savedWord);
        bool DeleteSavedWord(SavedWord savedWord);
        bool SavedWordExists(int id);
        bool SavedWordExists(string word, int userId);
        bool Save();
    }
}

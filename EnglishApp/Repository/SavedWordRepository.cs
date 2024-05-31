using EnglishApp.Data;
using EnglishApp.Dto;
using EnglishApp.Interface;
using EnglishApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Repository
{
    public class SavedWordRepository : ISavedWordRepository
    {
        private readonly EnglishAppContext _context;
        public SavedWordRepository(EnglishAppContext context)
        {
            _context = context;
        }
        public ICollection<WordMeaning> GetSavedWordDetails(string word)
        {
            return _context.WordMeanings
                .Include(y => y.WordDefinitions).Where(x => x.Word.Value == word).ToList();
        }
        public bool CreateSavedWord(SavedWord savedWord)
        {
            _context.SavedWords.Add(savedWord);
            return Save();
        }
        public bool DeleteSavedWord(SavedWord savedWord)
        {
            _context.Remove(savedWord);
            return Save();
        }   
        public SavedWord GetSavedWord(int id)
        {
            return _context.SavedWords.FirstOrDefault(x => x.Id == id);
        }
        public ICollection<SavedWord> GetSavedWordsByUser(int userId)
        {
            return _context.SavedWords.Where(x => x.UserId == userId).ToList();
        }
        public ICollection<Word> GetWordsByUser(int userId)
        {
            return _context.SavedWords.Where(x => x.UserId == userId).Select(x => x.Word).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool SavedWordExists(int id)
        {
            if (_context.SavedWords.Any(x => x.Id == id))
                return true;
            else return false;
        }
        public bool SavedWordExists(string wordString, int userId)
        {
            var word = _context.Words.FirstOrDefault( x => x.Value.ToUpper() == wordString.ToUpper());
            if (word == null)
                return false;
            var savedWordsByYser = GetSavedWordsByUser(userId);
            if(savedWordsByYser == null)
                return false;
            if (!GetSavedWordsByUser(userId).Any(x => x.WordId == word.Id))
            {
                return false; 
            }
            return true; 
        }
    }
}

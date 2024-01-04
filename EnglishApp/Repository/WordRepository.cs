using EnglishApp.Data;
using EnglishApp.Interface;
using EnglishApp.Models;
using Microsoft.EntityFrameworkCore;
using BLL;
using BLL.WordModels;
using System.Linq;

namespace EnglishApp.Repository
{
    public class WordRepository : IWordRepository
    {
        private readonly EnglishAppContext _context;
        public WordRepository(EnglishAppContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateWord(string wordString)
        {
            var wordApi = await WordService.GetWord(wordString);
            var word = new Word()
            {
                Value = wordString,
                Phonetic = wordApi.Phonetic
            };
            word.WordMeanings = new List<WordMeaning>();

            foreach (var meaning in wordApi.Meanings)
            {
                WordMeaning wordMeaning = new WordMeaning()
                {
                    Word = word,
                    PartOfSpeech = meaning.PartOfSpeech
                };
                wordMeaning.WordDefinitions = new List<WordDefinition>();
                foreach (var definition in meaning.Definitions)
                {
                    wordMeaning.WordDefinitions.Add(new WordDefinition()
                    {
                        WordMeaning = wordMeaning,
                        Value = definition.Definition,
                        Example = definition.Example
                    });
                }
                word.WordMeanings.Add(wordMeaning);
            }
            _context.Words.Add(word);
           
            return Save();
        }
        private WordMeaning GetWordMeaning(string partOfSpeech, int wordId)
        {
            return _context.WordMeanings.Where(x => x.WordId == wordId).FirstOrDefault(y => y.PartOfSpeech == partOfSpeech);
        }

        public Word GetWord(string word)
        {
            return _context.Words.FirstOrDefault(x => x.Value.ToUpper() == word.ToUpper());
        }
        public Word GetWord(int id)
        {
            return _context.Words.FirstOrDefault(x =>x.Id == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool WordExists(int id)
        {
            if (_context.Words.Any(x => x.Id == id))
                return true;
            else return false;
        }
        public bool WordExists(string word)
        {
            if (_context.Words.Any(x => x.Value.ToUpper() == word.ToUpper()))
                return true;
            else return false;
        }
    }
}


























//Save();
//WordMeaning wordMeanign;
//WordDefinition wordDefinition;
//foreach (var meaning in wordApi.Meanings)
//{
//    wordMeanign = new WordMeaning()
//    {
//        WordId = GetWord(wordString).Id,
//        PartOfSpeech = meaning.PartOfSpeech
//    };
//    _context.WordMeanings.Add(wordMeanign);
//    Save();
//    foreach(var definition in meaning.Definitions)
//    {
//        wordDefinition = new WordDefinition()
//        {
//            WordMeaningId = GetWordMeaning(meaning.PartOfSpeech, wordMeanign.WordId).Id,
//            Value = definition.Definition,
//            Example = definition.Example       
//        };
//        _context.WordDefinitions.Add(wordDefinition);
//        Save();
//    }
//}
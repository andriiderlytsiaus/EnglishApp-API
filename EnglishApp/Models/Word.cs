namespace EnglishApp.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public string?  Phonetic { get; set; }
        public ICollection<SavedWord>? SavedWords { get; set; } = null!;
        public ICollection<WordMeaning> WordMeanings { get; set; } = null!;
        
    }
}

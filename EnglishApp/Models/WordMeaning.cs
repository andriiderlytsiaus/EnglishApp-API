namespace EnglishApp.Models
{
    public class WordMeaning
    {
        public int Id { get; set; } 
        public string PartOfSpeech { get; set; } = null!;
        public int WordId { get; set; }
        public Word Word { get; set; } = null!;
        public ICollection<WordDefinition> WordDefinitions { get; set; } = null!;

    }
}

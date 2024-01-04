namespace EnglishApp.Models
{
    public class WordDefinition
    {
        public int Id { get; set; } 
        public string Value { get; set; } = null!;
        public string? Example { get; set; }
        public int WordMeaningId { get; set; }
        public WordMeaning WordMeaning { get; set; } = null!;
    }
}

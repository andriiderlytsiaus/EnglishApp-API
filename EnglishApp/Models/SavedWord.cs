namespace EnglishApp.Models
{
    public class SavedWord
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int WordId { get; set; }
        public Word Word { get; set; } = null!;
        
    }
}   

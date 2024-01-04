using EnglishApp.Models;

namespace EnglishApp.Dto
{
    public class SavedWordDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Word { get; set; }
    }
}

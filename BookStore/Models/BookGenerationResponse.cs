namespace BookStore.Models
{
    public class BookGenerationResponse
    {
        public List<Book> Books { get; set; } = new();
        public int TotalCount { get; set; }
        public bool HasMore { get; set; }
    }
}

namespace BookStore.Models
{
    public class Book
    {
        public int Index { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Authors { get; set; } = new();
        public string Publisher { get; set; } = string.Empty;
        public double Likes { get; set; }
        public List<Review> Reviews { get; set; } = new();
        public string CoverImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int Pages { get; set; }
    }
}

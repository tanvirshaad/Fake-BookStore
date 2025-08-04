namespace BookStore.Models
{
    public class BookGenerationRequest
    {
        public string Language { get; set; } = "en-US";
        public int Seed { get; set; } = 42;
        public double AverageLikes { get; set; } = 5.0;
        public double AverageReviews { get; set; } = 3.5;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

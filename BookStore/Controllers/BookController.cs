using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Services;
using CsvHelper;
using System.Globalization;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookGeneratorService _bookGeneratorService;

        public BookController(BookGeneratorService bookGeneratorService)
        {
            _bookGeneratorService = bookGeneratorService;
        }

        [HttpGet]
        public IActionResult GetBooks(
            [FromQuery] string country = "en_US",
            [FromQuery] int seed = 42,
            [FromQuery] double avgLikes = 3.7,
            [FromQuery] double avgReviews = 4.7,
            [FromQuery] int startIndex = 0,
            [FromQuery] int count = 20)
        {
            try
            {
                var books = _bookGeneratorService.GenerateBooks(country, seed, avgLikes, avgReviews, startIndex, count);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("export")]
        public IActionResult ExportToCsv(
            [FromQuery] string country = "en_US",
            [FromQuery] int seed = 42,
            [FromQuery] double avgLikes = 3.7,
            [FromQuery] double avgReviews = 4.7,
            [FromQuery] int pages = 1)
        {
            try
            {
                var allBooks = new List<Book>();
                for (int page = 0; page < pages; page++)
                {
                    var books = _bookGeneratorService.GenerateBooks(country, seed, avgLikes, avgReviews, page * 20, 20);
                    allBooks.AddRange(books);
                }

                using var writer = new StringWriter();
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                
                csv.WriteRecords(allBooks.Select(b => new
                {
                    Index = b.Index,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Authors = string.Join(", ", b.Authors),
                    Publisher = b.Publisher,
                    Likes = b.Likes,
                    ReviewCount = b.Reviews.Count
                }));

                var csvContent = writer.ToString();
                var fileName = $"books_{country}_{seed}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                
                return File(System.Text.Encoding.UTF8.GetBytes(csvContent), "text/csv", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
} 
using Q1.Models;

namespace Q1.DTOs
{
    public class MovieDTO
    {
        public MovieDTO()
        {
            Genres = new HashSet<Genre>();
            Stars = new HashSet<Star>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public string? ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string Language { get; set; } = null!;
        public int? ProducerId { get; set; }
        public int? DirectorId { get; set; }
        public string ProducerName { get; set; }
        public string DirectorName { get; set; }

        public ICollection<Genre> Genres { get; set; }
        public ICollection<Star> Stars { get; set; }
    }
}

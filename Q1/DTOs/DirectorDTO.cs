namespace Q1.DTOs
{
    public class DirectorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string dobString { get; set; }
        public string Nationality { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<MovieDTO> Movies { get; set; }
    }
}

namespace MovieWebApi.presentation.dto
{
    public class MovieDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Year { get; set; }

        public string? Poster { get; set; }
    }
}

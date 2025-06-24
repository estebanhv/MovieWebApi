using System.Text.Json.Serialization;

namespace MovieWebApi.presentation.dto
{
    public class MovieDTO
    {


        public int Id { get; set; }

        [JsonPropertyName("imdbID")]

        public string ImdbId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;


        public string? Year { get; set; }

        public string? Poster { get; set; }
    }
}

namespace MovieWebApi.presentation.dto
{
    public class FavoriteMovieDTO
    {
        public int UserId { get; set; }          // ID del usuario

        public string MovieId { get; set; }      // ID tipo string, compatible con imdbID

        public string Title { get; set; } = string.Empty;

        public string? Year { get; set; }

        public string? Poster { get; set; }

        public DateTime AddDate { get; set; }    // Fecha en que se agregó a favoritos
    }
}

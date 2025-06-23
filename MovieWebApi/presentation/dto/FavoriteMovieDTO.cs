namespace MovieWebApi.presentation.dto
{
    public class FavoriteMovieDTO
    {

        public string MovieId { get; set; }         // imdbID
        public string Title { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
        public DateTime AddDate { get; set; }       // Cuándo la agregó el usuario

    }
}

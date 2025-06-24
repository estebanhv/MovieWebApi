using MovieWebApi.Persistence.entities;

public class FavoriteMovieEntity
{
    public int UserId { get; set; }

    public string ImdbId { get; set; }

    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    public UserEntity User { get; set; }
    public MovieEntity Movie { get; set; }
}

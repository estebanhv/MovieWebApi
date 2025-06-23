using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebApi.Persistence.entities
{
    [Table("favorite_movie")]
    public class FavoriteMovieEntity
    {
        [Column("userId")]
        public string UserId { get; set; }

        [Column("movieId")]
        public string MovieId { get; set; }

        [Column("addedDate")]
        public DateTime AddedDate { get; set; } = DateTime.UtcNow; 

        public UserEntity User { get; set; }
        public MovieEntity Movie { get; set; }
    }
}

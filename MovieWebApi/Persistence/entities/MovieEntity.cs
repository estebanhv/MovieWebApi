using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MovieEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // ID interno para tu sistema

    [Required]
    [MaxLength(20)]
    public string ImdbId { get; set; } 

    public string Title { get; set; }
    public string? Year { get; set; }
    public string? Poster { get; set; }

    public ICollection<FavoriteMovieEntity> FavoriteMovies { get; set; }
}

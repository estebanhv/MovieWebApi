using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebApi.Persistence.entities
{
    [Table("movies")] // <-- esto define el nombre de la tabla

    public class MovieEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Year { get; set; } = string.Empty;

        public string? Poster { get; set; } = string.Empty;
    }
}

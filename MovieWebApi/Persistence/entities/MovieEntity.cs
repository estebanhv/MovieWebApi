using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebApi.Persistence.entities
{
    [Table("movies")] // <-- esto define el nombre de la tabla

    public class MovieEntity
    {
        [Column("id")]

        public int Id { get; set; }

        [Column("title")]


        public string Title { get; set; } = string.Empty;
        [Column("year")]

        public string? Year { get; set; } = string.Empty;

        [Column("poster")]

        public string? Poster { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebApi.Persistence.entities
{
    [Table("users")] 

    public class UserEntity


    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; }



        public ICollection<FavoriteMovieEntity> FavoriteMovies { get; set; }

    }
}

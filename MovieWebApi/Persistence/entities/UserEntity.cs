using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebApi.Persistence.entities
{
    [Table("users")] // <-- esto define el nombre de la tabla

    public class UserEntity


    {
        [Key]

        [Column("id")]

        public int Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }
    }
}

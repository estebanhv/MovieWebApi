using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDesktopApp.data.dto
{
    class FavoriteMovieDTO
    {
        public int UserId { get; set; }
        public string MovieId { get; set; } // imdbID
        public string Title { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
        public DateTime AddDate { get; set; }
    }
}

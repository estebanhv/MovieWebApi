using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDesktopApp.data.dto
{
    public class MovieDTO
    {
        public int Id { get; set; }     // imdbID
        public string Title { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
    }
}

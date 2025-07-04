﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDesktopApp.data.dto
{
    public class MovieDTO
    {
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }

        public bool IsFavorite { get; set; }

  


        public string ActionIcon => IsFavorite ? "❌" : "❤️";
    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDesktopApp.data.dto
{
    internal class LoginResponseDTO
    {
        public string Token { get; set; } // Solo si usas JWT

        public int UserId { get; set; } // ID del usuario, si es necesario

    }
}

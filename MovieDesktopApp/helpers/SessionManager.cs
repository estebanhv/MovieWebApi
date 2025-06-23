using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDesktopApp.helpers
{
    internal class SessionManager
    {

        public static string? Token { get; set; }
        public static string? UserEmail { get; set; }
        public static int? UserId { get; set; }
        public static string? Rol { get; set; }

        // Puedes agregar métodos útiles también
        public static bool IsLoggedIn => !string.IsNullOrEmpty(Token);

        public static void ClearSession()
        {
            Token = null;
            UserEmail = null;
            UserId = null;
            Rol = null;
        }

        public static HttpClient GetAuthorizedClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7037");

            if (!string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
            }

            return client;
        }


    }




}


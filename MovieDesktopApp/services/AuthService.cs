using MovieDesktopApp.data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieDesktopApp.services
{
    internal class AuthService
    {

        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7037") };
        }

        public async Task<LoginResponseDTO?> LoginAsync(string email, string password)
        {
            var dto = new UserDTO { Email = email, Password = password };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            }

            return null;
        }

    }
}

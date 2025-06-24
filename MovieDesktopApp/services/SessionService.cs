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
    internal class SessionService
    {

        private readonly HttpClient _httpClient;

        public SessionService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7037") };
        }

        public async Task<LoginResponseDTO?> LoginAsync(string email, string password)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7037") };

            var userDto = new UserDTO
            {
                Email = email,
                Password = password
            };

            var response = await httpClient.PostAsJsonAsync("/api/auth/login", userDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            }

            return null;
        }


        public async Task<bool> RegisterAsync(string email, string password)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7037");

            var dto = new
            {
                Email = email,
                Password = password
            };

            var response = await client.PostAsJsonAsync("/api/auth/register", dto);
            return response.IsSuccessStatusCode;
        }


    }
}

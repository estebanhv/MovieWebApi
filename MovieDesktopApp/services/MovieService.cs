using MovieDesktopApp.data.dto;
using MovieDesktopApp.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieDesktopApp.services
{
    internal class MovieService
    {
        private readonly HttpClient _client;

        public MovieService()
        {
            _client = SessionManager.GetAuthorizedClient();
        }

        public async Task<List<MovieDTO>> SearchMoviesAsync(string title)
        {
            var encoded = Uri.EscapeDataString(title);
            var response = await _client.GetFromJsonAsync<List<MovieDTO>>($"/api/Movie/search?title={encoded}");
            return response ?? new List<MovieDTO>();
        }
    }
}

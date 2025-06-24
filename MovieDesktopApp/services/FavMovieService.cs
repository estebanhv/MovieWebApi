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
    class FavMovieService
    {

        private readonly HttpClient _httpClient;

        public FavMovieService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7037") };
        }


        // Agregar a favoritos
        public async Task<bool> AddFavoriteAsync(FavoriteMovieDTO favorite)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/FavoriteMovie", favorite);
            return response.IsSuccessStatusCode;
        }

     
        // Obtener favoritos por usuario
        public async Task<List<MovieDTO>> GetFavoritesByUserAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<MovieDTO>>($"/api/FavoriteMovie/user/{userId}");
        }

        // Eliminar favorito
        public async Task<bool> RemoveFavoriteAsync(int userId, string imdbId)
        {
            var response = await _httpClient.DeleteAsync($"/api/FavoriteMovie/delete/{userId}/{imdbId}");
            return response.IsSuccessStatusCode;
        }

    }
}

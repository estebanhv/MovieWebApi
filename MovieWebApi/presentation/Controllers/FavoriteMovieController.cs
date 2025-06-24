// Rename the class to avoid conflict with the existing 'MovieController' class in the same namespace.
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.presentation.dto;
using MovieWebApi.service.interfaces;

namespace MovieWebApi.Presentation.Controllers
{
    [Route("api/FavoriteMovie")]
    [ApiController]
    public class FavoriteMovieController : ControllerBase
    {
        private readonly IFavoriteMovieService _favoriteService;

        public FavoriteMovieController(IFavoriteMovieService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        // POST: api/favoritemovie
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteMovieDTO dto)
        {
            var added = await _favoriteService.AddFavoriteAsync(dto);
            return added ? Ok("Película agregada a favoritos.") : Conflict("La película ya está en favoritos.");
        }

        // GET: api/favoritemovie/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetFavoritesByUser(int userId)
        {
            var favorites = await _favoriteService.GetFavoritesByUserAsync(userId);
            return Ok(favorites);
        }

        // DELETE: api/favoritemovie?userId=1&movieId=tt1234567
        [HttpDelete("delete/{userId}/{movieId}")]
        public async Task<IActionResult> RemoveFavorite(int userId, string movieId)
        {
            var removed = await _favoriteService.RemoveFavoriteAsync(userId, movieId);
            return removed ? Ok("Película eliminada de favoritos.") : NotFound("No se encontró la película en favoritos.");
        }

    }
}

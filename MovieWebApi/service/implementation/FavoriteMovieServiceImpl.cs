using Microsoft.EntityFrameworkCore;
using MovieWebApi.presentation.dto;
using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.entities;
using MovieWebApi.service.interfaces;

namespace MovieWebApi.service.implementation
{
    public class FavoriteMovieServiceImpl : IFavoriteMovieService
    {
        private readonly AppDbContext _context;
        private readonly IMovieService _movieService;

        public FavoriteMovieServiceImpl(AppDbContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        public async Task<bool> AddFavoriteAsync(FavoriteMovieDTO dto)
        {
            // Verifica si ya existe favorito con ese UserId + ImdbId
            var exists = await _context.FavoriteMovies
                .AnyAsync(f => f.UserId == dto.UserId && f.ImdbId == dto.MovieId);

            if (exists)
                return false;

            // Verifica si la película ya existe en base a ImdbId
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.ImdbId == dto.MovieId);

            // Si no existe, la crea usando MovieService
            if (movie == null)
            {
                var newMovieDto = new MovieDTO
                {
                    Id = dto.UserId,
                    ImdbId=dto.MovieId,
                    Title = dto.Title,
                    Year = dto.Year,
                    Poster = dto.Poster
                };

                await _movieService.CreateMovieAsync(newMovieDto);
            }

            // Crea el favorito usando ImdbId
            var favorite = new FavoriteMovieEntity
            {
                UserId = dto.UserId,
                ImdbId = dto.MovieId, //  Aquí usas imdbID, no el ID interno
                AddedDate = DateTime.UtcNow
            };

            _context.FavoriteMovies.Add(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MovieDTO>> GetFavoritesByUserAsync(int userId)
        {
            return await _context.FavoriteMovies
                .Where(f => f.UserId == userId)
                .Include(f => f.Movie)
                .Select(f => new MovieDTO
                {
                    Id = f.Movie.Id,
                    ImdbId = f.Movie.ImdbId,
                    Title = f.Movie.Title,
                    Year = f.Movie.Year,
                    Poster = f.Movie.Poster
                })
                .ToListAsync();
        }

        public async Task<bool> RemoveFavoriteAsync(int userId, string imdbId)
        {
            var favorite = await _context.FavoriteMovies
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ImdbId == imdbId);

            if (favorite == null)
                return false;

            _context.FavoriteMovies.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

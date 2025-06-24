using MovieWebApi.presentation.dto;

namespace MovieWebApi.service.interfaces
{
    public interface IFavoriteMovieService
    {

        Task<bool> AddFavoriteAsync(FavoriteMovieDTO dto);
        Task<List<MovieDTO>> GetFavoritesByUserAsync(int userId);
        Task<bool> RemoveFavoriteAsync(int userId, string movieId);

    }
}

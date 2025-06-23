using MovieWebApi.presentation.dto;

namespace MovieWebApi.service.interfaces
{
    public interface IMovieService
    {
   
        Task<IEnumerable<MovieDTO>> SearchMoviesAsync(string title);
        Task<List<MovieDTO>> FindAllAsync();
        Task<MovieDTO?> FindByIdAsync(long id);
        Task<MovieDTO> CreateMovieAsync(MovieDTO movieDTO);
        Task<MovieDTO?> UpdateMovieAsync(MovieDTO movieDTO, long id);
        Task<MovieDTO?> DeleteMovieAsync(long id);

    }
}

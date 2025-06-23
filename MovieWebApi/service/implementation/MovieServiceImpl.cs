using System.Text.Json;
using MovieWebApi.Persistence.entities;
using MovieWebApi.Persistence.repository.interfaces;
using MovieWebApi.presentation.dto;
using MovieWebApi.service.interfaces;
using MovieWebApi.service.mapper;

namespace MovieWebApi.service.implementation
{
    public class MovieServiceServiceImpl : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        private readonly MapperUtil<MovieDTO, MovieEntity> movieMapper = new(
            entity => new MovieDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Year = entity.Year,
                Poster = entity.Poster
            },
            dto => new MovieEntity
            {
                Id = dto.Id,
                Title = dto.Title,
                Year = dto.Year,
                Poster = dto.Poster
            }
        );

        public MovieServiceServiceImpl(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // 🔍 Búsqueda en OMDb API sin guardar en base de datos
        public async Task<IEnumerable<MovieDTO>> SearchMoviesAsync(string title)
        {
            var apiKey = "";
            var url = $"https://www.omdbapi.com/?apikey={apiKey}&s={Uri.EscapeDataString(title)}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<MovieDTO>();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<OmdbSearchResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Search ?? Enumerable.Empty<MovieDTO>();
        }

        // CRUD tradicional con base de datos

        public async Task<List<MovieDTO>> FindAllAsync()
        {
            var entities = await _movieRepository.GetAllAsync();
            return entities.Select(movieMapper.MapToDTO).ToList();
        }

        public async Task<MovieDTO?> FindByIdAsync(long id)
        {
            var entity = await _movieRepository.GetByIdAsync(id);
            return entity == null ? null : movieMapper.MapToDTO(entity);
        }

        public async Task<MovieDTO> CreateMovieAsync(MovieDTO movieDTO)
        {
            var entity = movieMapper.MapToEntity(movieDTO);
            await _movieRepository.AddAsync(entity);
            await _movieRepository.SaveChangesAsync();
            return movieMapper.MapToDTO(entity);
        }

        public async Task<MovieDTO?> UpdateMovieAsync(MovieDTO movieDTO, long id)
        {
            var existing = await _movieRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Title = movieDTO.Title;
            existing.Year = movieDTO.Year;
            existing.Poster = movieDTO.Poster;

            _movieRepository.Update(existing);
            await _movieRepository.SaveChangesAsync();
            return movieMapper.MapToDTO(existing);
        }

        public async Task<MovieDTO?> DeleteMovieAsync(long id)
        {
            var existing = await _movieRepository.GetByIdAsync(id);
            if (existing == null) return null;

            _movieRepository.Delete(existing);
            await _movieRepository.SaveChangesAsync();
            return movieMapper.MapToDTO(existing);
        }

        
    }
}

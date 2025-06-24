using Microsoft.EntityFrameworkCore;
using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.repository.interfaces;
using MovieWebApi.presentation.dto;
using MovieWebApi.service.interfaces;
using MovieWebApi.service.mapper;
using System.Text.Json;

namespace MovieWebApi.service.implementation
{
    public class MovieServiceServiceImpl : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly AppDbContext _context;

        private readonly string _apiKey;


        private readonly MapperUtil<MovieDTO, MovieEntity> movieMapper = new(
            entity => new MovieDTO
            {
                Id = entity.Id,
                ImdbId=entity.ImdbId,
                Title = entity.Title,
                Year = entity.Year,
                Poster = entity.Poster
            },
            dto => new MovieEntity
            {
                Id = dto.Id,
                ImdbId= dto.ImdbId,
                Title = dto.Title,
                Year = dto.Year,
                Poster = dto.Poster
            }
        );

        public MovieServiceServiceImpl(IMovieRepository movieRepository, AppDbContext context, IConfiguration configuration)
        {
            _movieRepository = movieRepository;
            _context = context;
            _apiKey = configuration["OmdbApi:ApiKey"];

        }

        //  Búsqueda en OMDb API sin guardar en base de datos
        public async Task<IEnumerable<MovieDTO>> SearchMoviesAsync(string title)
        {
            var apiKey = "";
            var url = $"https://www.omdbapi.com/?apikey={_apiKey}&s={Uri.EscapeDataString(title)}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<MovieDTO>();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<OmdbSearchResultDTO>(json, new JsonSerializerOptions
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

        public async Task<MovieDTO?> FindByIdAsync(string id)
        {
            var entity = await _movieRepository.GetByIdAsync(id);
            return entity == null ? null : movieMapper.MapToDTO(entity);
        }

        public async Task<MovieDTO> CreateMovieAsync(MovieDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ImdbId))
                throw new ArgumentException("ImdbId (dto.Id) is required.");

            // Evita duplicados por ImdbId
            var existing = await _context.Movies.FirstOrDefaultAsync(m => m.ImdbId == dto.ImdbId);
            if (existing != null)
                return movieMapper.MapToDTO(existing);

            var movie = new MovieEntity
            {
                ImdbId = dto.ImdbId,           //  Usar dto.Id como imdbID
                Title = dto.Title,
                Year = dto.Year,
                Poster = dto.Poster
                //  No poner el Id (autoincremental)
            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movieMapper.MapToDTO(movie);
        }



        public async Task<MovieDTO?> UpdateMovieAsync(MovieDTO movieDTO, string id)
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

        public async Task<MovieDTO?> DeleteMovieAsync(string id)
        {
            var existing = await _movieRepository.GetByIdAsync(id);
            if (existing == null) return null;

            _movieRepository.Delete(existing);
            await _movieRepository.SaveChangesAsync();
            return movieMapper.MapToDTO(existing);
        }

        
    }
}

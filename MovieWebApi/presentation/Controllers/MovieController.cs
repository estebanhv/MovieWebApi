using Microsoft.AspNetCore.Mvc;
using MovieWebApi.presentation.dto;
using MovieWebApi.service.interfaces;

namespace MovieWebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAll()
        {
            var movies = await _movieService.FindAllAsync();
            return Ok(movies);
        }

        // GET: api/movie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetById(long id)
        {
            var movie = await _movieService.FindByIdAsync(id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        // POST: api/movie
        [HttpPost]
        public async Task<ActionResult<MovieDTO>> Create([FromBody] MovieDTO movieDto)
        {
            var created = await _movieService.CreateMovieAsync(movieDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/movie/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDTO>> Update(long id, [FromBody] MovieDTO movieDto)
        {
            var updated = await _movieService.UpdateMovieAsync(movieDto, id);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/movie/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDTO>> Delete(long id)
        {
            var deleted = await _movieService.DeleteMovieAsync(id);
            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}

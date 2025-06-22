using MovieWebApi.Persistence.entities;

namespace MovieWebApi.Persistence.repository.interfaces
{
    public interface IMovieRepository : IGenericRepository<MovieEntity>
    {
        // Define any additional methods specific to MovieEntity if needed
        // For example, you might want to add methods for searching or filtering movies
    }
   
}

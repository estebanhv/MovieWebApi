using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.entities;
using MovieWebApi.Persistence.repository.interfaces;

namespace MovieWebApi.Persistence.repository
{
    public class MovieRepository : GenericRepository<MovieEntity>, IMovieRepository
    {
        public MovieRepository(AppDbContext context) : base(context)
        {
        }

        
    }
}
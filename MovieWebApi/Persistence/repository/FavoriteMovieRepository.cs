using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.entities;
using MovieWebApi.Persistence.repository.interfaces;

namespace MovieWebApi.Persistence.repository
{
    public class FavoriteMovieRepository : GenericRepository<FavoriteMovieEntity>, IFavoriteMovieRepository
    {
        public FavoriteMovieRepository(AppDbContext context) : base(context)
        {
        }
        // Métodos específicos de FavoriteMovieEntity, si es necesario
    }
    
}

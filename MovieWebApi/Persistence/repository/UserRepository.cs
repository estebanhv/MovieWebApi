using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.entities;
using MovieWebApi.Persistence.repository.interfaces;

namespace MovieWebApi.Persistence.repository
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        // Métodos específicos de MovieEntity, si es necesario
    }
}

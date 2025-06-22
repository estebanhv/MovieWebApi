
using Microsoft.EntityFrameworkCore;
using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.repository.interfaces;

namespace MovieWebApi.Persistence.repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {// 👇 Aquí defines el DbContext y el DbSet que usarás en todos los métodos
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        // 👇 Constructor recibe el contexto e inicializa el DbSet genérico
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // <-- esto hace que funcione con cualquier entidad
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

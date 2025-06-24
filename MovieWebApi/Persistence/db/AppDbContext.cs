using Microsoft.EntityFrameworkCore;
using MovieWebApi.Persistence.entities;

namespace MovieWebApi.Persistence.db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<FavoriteMovieEntity> FavoriteMovies { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteMovieEntity>()
                .HasKey(f => new { f.UserId, f.ImdbId }); 

            modelBuilder.Entity<FavoriteMovieEntity>()
                .HasOne(f => f.User)
                .WithMany(u => u.FavoriteMovies)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<FavoriteMovieEntity>()
                .HasOne(f => f.Movie)
                .WithMany(m => m.FavoriteMovies)
                .HasForeignKey(f => f.ImdbId)
                .HasPrincipalKey(m => m.ImdbId); 
        }



    }
}

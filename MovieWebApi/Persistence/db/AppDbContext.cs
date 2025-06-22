using Microsoft.EntityFrameworkCore;
using MovieWebApi.Persistence.entities;
using System.Collections.Generic;

namespace MovieWebApi.Persistence.db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<UserEntity> Users { get; set; }


    }
}
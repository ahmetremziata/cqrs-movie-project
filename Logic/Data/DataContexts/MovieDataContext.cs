using Logic.Data.Entities;
using Logic.Data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Logic.Data.DataContexts
{
    public class MovieDataContext : DataContextBase
    {
        public MovieDataContext(DbContextOptions<MovieDataContext> options) : base(options)
        {
        }
        
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieType> MovieTypes { get; set; }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDataContext _dataContext;

        public MovieRepository(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _dataContext.Movies.ToListAsync();
        }
        
        public async Task<Movie> GetMovieById(int id)
        {
            return await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}
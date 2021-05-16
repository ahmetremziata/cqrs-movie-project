using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Data.Entities;

namespace Logic.Data.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();
        Task<Movie> GetMovieById(int id);
    }
}
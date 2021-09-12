using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Model;

namespace Logic.Business.Service.Crud
{
    public interface IMovieService
    {
        Task<List<CrudMovieModel>> GetMovies();
        Task<Result> EditMovie(CrudMovieModel movie);
        Task<InsertResult> InsertMovie(CrudMovieModel movie);
    }
}
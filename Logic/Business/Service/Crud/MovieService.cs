using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Data.Repositories.Interfaces;
using Logic.Model;
using Microsoft.EntityFrameworkCore;

namespace Logic.Business.Service.Crud
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly MovieDataContext _dataContext;

        public MovieService(IMovieRepository movieRepository, MovieDataContext dataContext)
        {
            _movieRepository = movieRepository;
            _dataContext = dataContext;
        }
        
        public async Task<List<CrudMovieModel>> GetMovies()
        {
            IReadOnlyList<Movie> movies = await _movieRepository.GetMovies();
            List<CrudMovieModel> response = movies.Select(x => ConvertToDto(x)).ToList();
            return response;
        }

        public async Task<Result> EditMovie(CrudMovieModel crudMovieModel)
        {
            Movie movie = await _movieRepository.GetMovieById(crudMovieModel.Id);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {crudMovieModel.Id}");
            }
            
            var existingMovie = await _dataContext.Movies.SingleOrDefaultAsync(item =>
                item.Name == crudMovieModel.Name && item.OriginalName == crudMovieModel.OriginalName && item.Id != crudMovieModel.Id);

            if (existingMovie != null)
            {
                return Result.Failure(
                    $"Movie already found for Name: {crudMovieModel.Name} OriginalName: {crudMovieModel.OriginalName}");
            } 
            
            movie.OriginalName = crudMovieModel.OriginalName;
            movie.Description = crudMovieModel.Description;
            movie.Name = crudMovieModel.Name;
            movie.PosterUrl = crudMovieModel.PosterUrl;
            movie.TotalMinute = crudMovieModel.TotalMinute;
            movie.VisionEntryDate = crudMovieModel.VisionEntryDate;
            movie.ConstructionYear = crudMovieModel.ConstructionYear;
            _dataContext.SaveChanges();
            return Result.Success();
        }
        
        private CrudMovieModel ConvertToDto(Movie movie)
        {
            return new CrudMovieModel
            {
                Id = movie.Id,
                Name = movie.Name,
                OriginalName = movie.OriginalName,
                ConstructionYear = movie.ConstructionYear,
                TotalMinute = movie.TotalMinute,
                IsActive = movie.IsActive,
                IsSynchronized = movie.IsSynchronized
            };
        }
    }
}
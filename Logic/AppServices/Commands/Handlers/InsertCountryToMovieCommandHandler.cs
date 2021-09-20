using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public class InsertCountryToMovieCommandHandler : ICommandHandler<InsertCountryToMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertCountryToMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> Handle(InsertCountryToMovieCommand command)
        {
            Movie movie = await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }
            
            MovieCountry movieCountry =  await _dataContext.MovieCountries.FirstOrDefaultAsync(item => item.MovieId == command.MovieId 
                && item.CountryId == command.CountryId);

            if (movieCountry != null)
            {
                return Result.Failure($"Country with movieId: {command.MovieId} countryId: {command.CountryId} already exists so it cannot be added");
            }
            
            movie.IsSynchronized = false;
            
            MovieCountry newMovieCountry = new MovieCountry()
            {
                MovieId = command.MovieId, 
                CountryId = command.CountryId
            };

            await _dataContext.MovieCountries.AddAsync(newMovieCountry);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
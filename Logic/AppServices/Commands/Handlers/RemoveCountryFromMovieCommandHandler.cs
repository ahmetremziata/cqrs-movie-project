using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class RemoveCountryFromMovieCommandHandler : ICommandHandler<RemoveCountryFromMovieCommand>
    {
        private readonly MovieDataContext _dataContext;
        
        public RemoveCountryFromMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(RemoveCountryFromMovieCommand command)
        {
            Movie movie = await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }
            
            
            MovieCountry movieCountry =  await _dataContext.MovieCountries.FirstOrDefaultAsync(item => item.MovieId == command.MovieId 
                && item.CountryId == command.CountryId);

            if (movieCountry == null)
            {
                return Result.Failure($"No country in movie found for movieId: {command.MovieId} countryId: {command.CountryId}");
            }
            
            movie.IsSynchronized = false;
            _dataContext.MovieCountries.Remove(movieCountry);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
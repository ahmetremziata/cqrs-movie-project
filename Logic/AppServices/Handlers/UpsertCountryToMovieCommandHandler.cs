using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Handlers
{
    public sealed class UpsertCountryToMovieCommandHandler : ICommandHandler<UpsertCountryToMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public UpsertCountryToMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(UpsertCountryToMovieCommand command)
        {
            //TODO: Validation for control distinct!
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }

            List<MovieCountry> movieCountries =
                await _dataContext.MovieCountries.Where(item => item.MovieId == movie.Id).ToListAsync();

            //Delete all list first!
            foreach (var movieCountry in movieCountries)
            {
                _dataContext.MovieCountries.Remove(movieCountry);
            }
            
            foreach (var countryId in command.CountryIds)
            {
                MovieCountry movieCountry = new MovieCountry() {MovieId = movie.Id, CountryId = countryId};
                await _dataContext.MovieCountries.AddAsync(movieCountry);
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
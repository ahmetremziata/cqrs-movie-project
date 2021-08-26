using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class DeleteCountryCommandHandler : ICommandHandler<DeleteCountryCommand>
    {
        private readonly MovieDataContext _dataContext;

        public DeleteCountryCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(DeleteCountryCommand command)
        {
            Country country =  await _dataContext.Countries.FirstOrDefaultAsync(item => item.Id == command.CountryId);
            if (country == null)
            {
                return Result.Failure($"No Country found for Id {command.CountryId}");
            }
            
            _dataContext.Countries.Remove(country);

            List<MovieCountry> movieCountries =
                await _dataContext.MovieCountries.Where(item => item.CountryId == command.CountryId).ToListAsync();
            
            List<int> movieIds = movieCountries.Select(item => item.MovieId).Distinct().ToList();

            foreach (var movieId in movieIds)
            {
                Movie movie =  await _dataContext.Movies.SingleOrDefaultAsync(item => item.Id == movieId);
                movie.IsSynchronized = false;
            }

            foreach (var movieCountry in movieCountries)
            {
                _dataContext.MovieCountries.Remove(movieCountry);
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public class EditCountryInfoCommandHandler : ICommandHandler<EditCountryInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public EditCountryInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> Handle(EditCountryInfoCommand command)
        {
            Country country = await _dataContext.Countries.SingleOrDefaultAsync(item => item.Id == command.CountryId);

            if (country == null)
            {
                return Result.Failure($"No country found for Id {command.CountryId}");
            }
            
            var existingCountry = await _dataContext.Countries.SingleOrDefaultAsync(item =>
                item.Name == command.Name);
            
            if (existingCountry != null)
            {
                return Result.Failure($"Country already found for name: {command.Name}");
            }
            
            country.Name = command.Name;
            
            List<MovieCountry> movieCountries =
                await _dataContext.MovieCountries.Where(item => item.CountryId == country.Id).ToListAsync();

            foreach (var movieCountry in movieCountries)
            {
                Movie movie = await _dataContext.Movies.SingleOrDefaultAsync(item => item.Id == movieCountry.MovieId);

                if (movie != null)
                {
                    movie.IsSynchronized = false;
                }
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
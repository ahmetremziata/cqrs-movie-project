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
    public sealed class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public DeleteMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(DeleteMovieCommand command)
        {
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.Id);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.Id}");
            }
            _dataContext.Movies.Remove(movie);

            List<MoviePerson> moviePersons =
                await _dataContext.MoviePersons.Where(item => item.MovieId == command.Id).ToListAsync();

            foreach (var moviePerson in moviePersons)
            {
                _dataContext.MoviePersons.Remove(moviePerson);
            }
            
            List<MovieCountry> movieCountries =
                await _dataContext.MovieCountries.Where(item => item.MovieId == command.Id).ToListAsync();

            foreach (var movieCountry in movieCountries)
            {
                _dataContext.MovieCountries.Remove(movieCountry);
            }
            
            List<MovieType> movieTypes =
                await _dataContext.MovieTypes.Where(item => item.MovieId == command.Id).ToListAsync();

            foreach (var movieType in movieTypes)
            {
                _dataContext.MovieTypes.Remove(movieType);
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
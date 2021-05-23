using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class UpsertPersonToMovieCommandHandler : ICommandHandler<UpsertPersonToMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public UpsertPersonToMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(UpsertPersonToMovieCommand command)
        {
            //TODO: Validation for control distinct!
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }

            List<MoviePerson> moviePersons =
                await _dataContext.MoviePersons.Where(item => item.MovieId == movie.Id).ToListAsync();

            //Delete all list first!
            foreach (var moviePerson in moviePersons)
            {
                _dataContext.MoviePersons.Remove(moviePerson);
            }
            
            foreach (var moviePerson in command.MoviePersons)
            {
                MoviePerson moviePersonEntity = new MoviePerson() {MovieId = movie.Id, RoleId = moviePerson.RoleId, PersonId = moviePerson.PersonId};
                await _dataContext.MoviePersons.AddAsync(moviePersonEntity);
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
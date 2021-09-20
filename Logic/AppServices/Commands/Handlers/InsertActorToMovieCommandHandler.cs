using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public class InsertActorToMovieCommandHandler : ICommandHandler<InsertActorToMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertActorToMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> Handle(InsertActorToMovieCommand command)
        {
            Movie movie = await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }
            
            MoviePerson moviePerson =  await _dataContext.MoviePersons.FirstOrDefaultAsync(item => item.MovieId == command.MovieId 
                && item.PersonId == command.PersonId 
                && item.RoleId == command.RoleId);

            if (moviePerson != null)
            {
                return Result.Failure($"Person with movieId: {command.MovieId} personId: {command.PersonId} roleId: {command.RoleId} already exists so it cannot be added");
            }
            
            movie.IsSynchronized = false;
            
            MoviePerson newMoviePerson = new MoviePerson
            {
                MovieId = command.MovieId, 
                PersonId = command.PersonId, 
                RoleId = command.RoleId,
                CharacterName = command.CharacterName
            };

            await _dataContext.MoviePersons.AddAsync(newMoviePerson);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
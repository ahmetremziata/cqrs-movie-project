using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class RemoveActorFromMovieCommandHandler : ICommandHandler<RemoveActorFromMovieCommand>
    {
        private readonly MovieDataContext _dataContext;
        
        public RemoveActorFromMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(RemoveActorFromMovieCommand command)
        {
            Movie movie = await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }

            movie.IsSynchronized = false;
            
            MoviePerson moviePerson =  await _dataContext.MoviePersons.FirstOrDefaultAsync(item => item.Id == command.MovieId 
                && item.PersonId == command.PersonId 
                && item.RoleId == command.RoleId);

            if (moviePerson == null)
            {
                return Result.Failure($"No person in movie found for movieId: {command.MovieId} personId: {command.PersonId} roleId: {command.RoleId}");
            }
            
            _dataContext.MoviePersons.Remove(moviePerson);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
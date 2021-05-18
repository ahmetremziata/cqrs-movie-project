using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Handlers
{
    public sealed class DeactivateMovieCommandHandler : ICommandHandler<DeactivateMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public DeactivateMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(DeactivateMovieCommand command)
        {
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }

            if (!movie.IsActive)
            {
                return Result.Failure($"Movie already passive for movieId {command.MovieId}");
            }

            movie.IsActive = false;
            await _dataContext.SaveChangesAsync();
            
            //TODO: Add fat event for activated and send event
            return Result.Success();
        }
    }
}
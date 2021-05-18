using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Handlers
{
    public sealed class ActivateMovieCommandHandler : ICommandHandler<ActivateMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public ActivateMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(ActivateMovieCommand command)
        {
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }

            if (movie.IsActive)
            {
                return Result.Failure($"Movie already active for movieId {command.MovieId}");
            }

            movie.IsActive = true;
            await _dataContext.SaveChangesAsync();
            
            //TODO: Add fat event for activated and send event
            return Result.Success();
        }
    }
}
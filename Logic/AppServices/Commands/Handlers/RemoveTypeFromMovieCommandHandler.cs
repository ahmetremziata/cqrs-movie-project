using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class RemoveTypeFromMovieCommandHandler : ICommandHandler<RemoveTypeFromMovieCommand>
    {
        private readonly MovieDataContext _dataContext;
        
        public RemoveTypeFromMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(RemoveTypeFromMovieCommand command)
        {
            Movie movie = await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }
            
            
            MovieType movieType =  await _dataContext.MovieTypes.FirstOrDefaultAsync(item => item.MovieId == command.MovieId 
                && item.TypeId == command.TypeId);

            if (movieType == null)
            {
                return Result.Failure($"No type in movie found for movieId: {command.MovieId} typeId: {command.TypeId}");
            }
            
            movie.IsSynchronized = false;
            _dataContext.MovieTypes.Remove(movieType);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
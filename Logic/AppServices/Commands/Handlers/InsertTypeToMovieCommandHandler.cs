using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public class InsertTypeToMovieCommandHandler : ICommandHandler<InsertTypeToMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertTypeToMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> Handle(InsertTypeToMovieCommand command)
        {
            Movie movie = await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }
            
            MovieType movieType =  await _dataContext.MovieTypes.FirstOrDefaultAsync(item => item.MovieId == command.MovieId 
                && item.TypeId == command.TypeId);

            if (movieType != null)
            {
                return Result.Failure($"Type with movieId: {command.MovieId} typeId: {command.TypeId} already exists so it cannot be added");
            }
            
            movie.IsSynchronized = false;
            
            MovieType newMovieType = new MovieType
            {
                MovieId = command.MovieId, 
                TypeId = command.TypeId
            };

            await _dataContext.MovieTypes.AddAsync(newMovieType);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
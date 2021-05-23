using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class UpsertTypeToMovieCommandHandler : ICommandHandler<UpsertTypeToMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public UpsertTypeToMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(UpsertTypeToMovieCommand command)
        {
            //TODO: Validation for control distinct!
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }

            List<MovieType> movieTypes =
                await _dataContext.MovieTypes.Where(item => item.MovieId == movie.Id).ToListAsync();

            //Delete all list first!
            foreach (var movieType in movieTypes)
            {
                _dataContext.MovieTypes.Remove(movieType);
            }
            
            foreach (var typeId in command.TypeIds)
            {
                MovieType movieType = new MovieType() {MovieId = movie.Id, TypeId = typeId};
                await _dataContext.MovieTypes.AddAsync(movieType);
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
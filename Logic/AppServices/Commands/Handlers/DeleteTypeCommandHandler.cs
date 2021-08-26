using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class DeleteTypeCommandHandler : ICommandHandler<DeleteTypeCommand>
    {
        private readonly MovieDataContext _dataContext;

        public DeleteTypeCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(DeleteTypeCommand command)
        {
            Type type =  await _dataContext.Types.FirstOrDefaultAsync(item => item.Id == command.TypeId);
            if (type == null)
            {
                return Result.Failure($"No Type found for Id {command.TypeId}");
            }
            
            _dataContext.Types.Remove(type);

            List<MovieType> movieTypes =
                await _dataContext.MovieTypes.Where(item => item.TypeId == command.TypeId).ToListAsync();
            
            List<int> movieIds = movieTypes.Select(item => item.MovieId).Distinct().ToList();

            foreach (var movieId in movieIds)
            {
                Movie movie =  await _dataContext.Movies.SingleOrDefaultAsync(item => item.Id == movieId);
                movie.IsSynchronized = false;
            }

            foreach (var movieType in movieTypes)
            {
                _dataContext.MovieTypes.Remove(movieType);
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
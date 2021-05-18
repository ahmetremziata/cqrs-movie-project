using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Data.Repositories.Interfaces;
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
            return Result.Success();
        }
    }
}
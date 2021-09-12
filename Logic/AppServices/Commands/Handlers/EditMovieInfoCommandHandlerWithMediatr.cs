using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class EditMovieInfoCommandHandlerWithMediatr : IRequestHandler<EditMovieInfoCommandWithMediatr, Result>
    {
        private readonly MovieDataContext _dataContext;

        public EditMovieInfoCommandHandlerWithMediatr(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(EditMovieInfoCommandWithMediatr command, CancellationToken cancellationToken)
        {
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.Id);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.Id}");
            }
            
            var existingMovie = await _dataContext.Movies.SingleOrDefaultAsync(item =>
                item.Name == command.Name && item.OriginalName == command.OriginalName && item.Id != command.Id);

            if (existingMovie != null)
            {
                return Result.Failure(
                    $"Movie already found for Name: {command.Name} OriginalName: {command.OriginalName}");
            } 
            
            movie.OriginalName = command.OriginalName;
            movie.Description = command.Description;
            movie.Name = command.Name;
            movie.PosterUrl = command.PosterUrl;
            movie.TotalMinute = command.TotalMinute;
            movie.VisionEntryDate = command.VisionEntryDate;
            movie.ConstructionYear = command.ConstructionYear;
            movie.IsSynchronized = false;
            await _dataContext.SaveChangesAsync();
            
            return Result.Success();
        }
    }
}
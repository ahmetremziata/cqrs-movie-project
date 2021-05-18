using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Handlers
{
    public sealed class EditMovieInfoCommandHandler : ICommandHandler<EditMovieInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public EditMovieInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(EditMovieInfoCommand command)
        {
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.Id);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.Id}");
            }
            movie.OriginalName = command.OriginalName;
            movie.Description = command.Description;
            movie.Name = command.Name;
            movie.PosterUrl = command.PosterUrl;
            movie.TotalMinute = command.TotalMinute;
            movie.VisionEntryDate = command.VisionEntryDate;
            movie.ConstructionYear = command.ConstructionYear;
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
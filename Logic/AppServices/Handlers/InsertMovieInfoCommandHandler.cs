using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Handlers
{
    public sealed class InsertMovieInfoCommandHandler : ICommandHandler<InsertMovieInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertMovieInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(InsertMovieInfoCommand command)
        {
            var existingMovie = await _dataContext.Movies.SingleOrDefaultAsync(item =>
                item.Name == command.Name && item.OriginalName == command.OriginalName &&
                item.ConstructionYear == command.ConstructionYear);

            if (existingMovie != null)
            {
                return Result.Failure($"Movie already found for Name: {command.Name} OriginalName: {command.OriginalName} ConstructionYear: {command.ConstructionYear}");
            }
                
            Movie movie = new Movie
            {
                OriginalName = command.OriginalName,
                Description = command.Description,
                Name = command.Name,
                PosterUrl = command.PosterUrl,
                TotalMinute = command.TotalMinute,
                VisionEntryDate = command.VisionEntryDate,
                ConstructionYear = command.ConstructionYear,
                CreatedOn = DateTime.Now,
                IsActive = false
            };
            
            await _dataContext.Movies.AddAsync(movie);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
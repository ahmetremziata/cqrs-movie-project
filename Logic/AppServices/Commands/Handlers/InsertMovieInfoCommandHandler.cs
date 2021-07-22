using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Dtos;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class InsertMovieInfoCommandHandler : IInsertCommandHandler<InsertMovieInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertMovieInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<InsertResult> Handle(InsertMovieInfoCommand command)
        {
            var existingMovie = await _dataContext.Movies.SingleOrDefaultAsync(item =>
                item.Name == command.Name && item.OriginalName == command.OriginalName &&
                item.ConstructionYear == command.ConstructionYear);

            if (existingMovie != null)
            {
                return new InsertResult()
                {
                    Result = Result.Failure(
                        $"Movie already found for Name: {command.Name} OriginalName: {command.OriginalName} ConstructionYear: {command.ConstructionYear}")
                };
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
            return new InsertResult()
            {
                Result = Result.Success(), InsertResponse = new InsertResponse()
                {
                    Id = movie.Id
                }
            };
        }
    }
}
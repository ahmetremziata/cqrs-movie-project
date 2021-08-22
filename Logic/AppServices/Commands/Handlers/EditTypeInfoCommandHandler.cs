using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class EditTypeInfoCommandHandler : ICommandHandler<EditTypeInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public EditTypeInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(EditTypeInfoCommand command)
        {
            Type type =  await _dataContext.Types.FirstOrDefaultAsync(item => item.Id == command.TypeId);
            
            if (type == null)
            {
                return Result.Failure($"No type found for Id {command.TypeId}");
            }
            
            var existingType = await _dataContext.Types.SingleOrDefaultAsync(item =>
                item.Name == command.Name);
            
            if (existingType != null)
            {
                return Result.Failure($"Type already found for name: {command.Name}");
            }
            
            type.Name = command.Name;
            
            List<MovieType> movieTypes =
                await _dataContext.MovieTypes.Where(item => item.TypeId == type.Id).ToListAsync();

            foreach (var movieType in movieTypes)
            {
                Movie movie = await _dataContext.Movies.SingleOrDefaultAsync(item => item.Id == movieType.MovieId);

                if (movie != null)
                {
                    movie.IsSynchronized = false;
                }
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
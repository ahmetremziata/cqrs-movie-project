using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand>
    {
        private readonly MovieDataContext _dataContext;

        public DeletePersonCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(DeletePersonCommand command)
        {
            Person person =  await _dataContext.Persons.FirstOrDefaultAsync(item => item.Id == command.Id);
            if (person == null)
            {
                return Result.Failure($"No person found for Id {command.Id}");
            }
            _dataContext.Persons.Remove(person);

            List<MoviePerson> moviePersons =
                await _dataContext.MoviePersons.Where(item => item.PersonId == command.Id).ToListAsync();

            foreach (var moviePerson in moviePersons)
            {
                _dataContext.MoviePersons.Remove(moviePerson);
            }
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
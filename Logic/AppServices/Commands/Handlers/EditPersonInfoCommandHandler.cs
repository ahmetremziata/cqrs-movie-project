using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class EditPersonInfoCommandHandler : ICommandHandler<EditPersonInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public EditPersonInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(EditPersonInfoCommand command)
        {
            Person person =  await _dataContext.Persons.FirstOrDefaultAsync(item => item.Id == command.Id);
            if (person == null)
            {
                return Result.Failure($"No person found for Id {command.Id}");
            }
            
            var existingPerson = await _dataContext.Persons.SingleOrDefaultAsync(item =>
                item.Name == command.Name && item.RealName == command.RealName && item.Id != command.Id);

            if (existingPerson != null)
            {
                return Result.Failure(
                    $"Person already found for Name: {command.Name} RealName: {command.RealName}");

            } 
            
            person.Name = command.Name;
            person.RealName = command.RealName;
            person.Biography = command.Biography;
            person.AvatarUrl = command.AvatarUrl;
            person.BirthDate = command.BirthDate;
            person.BirthPlace = command.BirthPlace;
            person.DeathDate = command.DeathDate;
            person.DeathPlace = command.DeathPlace;
            
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public class InsertPersonInfoCommandHandler : ICommandHandler<InsertPersonInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertPersonInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async  Task<Result> Handle(InsertPersonInfoCommand command)
        {
            var existingPerson = await _dataContext.Persons.SingleOrDefaultAsync(item =>
                item.Name == command.Name && item.RealName == command.RealName);

            if (existingPerson != null)
            {
                return Result.Failure($"Person already found for Name: {command.Name} RealName: {command.RealName}");
            }
            
            Person person = new Person
            {
                Name = command.Name,
                RealName = command.RealName,
                Biography = command.Biography,
                BirthDate = command.BirthDate,
                BirthPlace = command.BirthPlace,
                DeathDate = command.DeathDate,
                DeathPlace = command.DeathPlace,
                AvatarUrl = command.AvatarUrl,
                CreatedOn = DateTime.Now,
            };
            
            await _dataContext.Persons.AddAsync(person);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
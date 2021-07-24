using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public class InsertRoleInfoCommandHandler : ICommandHandler<InsertRoleInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertRoleInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(InsertRoleInfoCommand command)
        {
            var existingRole = await _dataContext.Roles.SingleOrDefaultAsync(item => item.Name == command.Name);

            if (existingRole != null)
            {
                return Result.Failure($"Role already found for name: {command.Name}");
            }

            Role role = new Role {Name = command.Name, CreatedOn = DateTime.Now};

            await _dataContext.Roles.AddAsync(role);
            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
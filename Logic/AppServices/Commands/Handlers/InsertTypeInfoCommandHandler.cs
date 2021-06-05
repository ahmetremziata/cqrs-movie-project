using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class InsertTypeInfoCommandHandler : ICommandHandler<InsertTypeInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertTypeInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(InsertTypeInfoCommand command)
        {
            var existingMovie = await _dataContext.Types.SingleOrDefaultAsync(item =>
                item.Name == command.Name);

            if (existingMovie != null)
            {
                return Result.Failure($"Type already found for name: {command.Name}");
            }
                
            Data.Entities.Type type = new Data.Entities.Type
            {
                Name = command.Name,
                CreatedOn = DateTime.Now
            };
            
            await _dataContext.Types.AddAsync(type);
            await _dataContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
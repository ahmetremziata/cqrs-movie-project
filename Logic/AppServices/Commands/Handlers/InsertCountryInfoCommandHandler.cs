using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Commands.Handlers
{
    public class InsertCountryInfoCommandHandler : ICommandHandler<InsertCountryInfoCommand>
    {
        private readonly MovieDataContext _dataContext;

        public InsertCountryInfoCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(InsertCountryInfoCommand command)
        {
            var existingCountry = await _dataContext.Countries.SingleOrDefaultAsync(item => item.Name == command.Name);

            if (existingCountry != null)
            {
                return Result.Failure($"Country already found for name: {command.Name}");
            }

            Country country = new Country {Name = command.Name, CreatedOn = DateTime.Now};

            await _dataContext.Countries.AddAsync(country);
            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
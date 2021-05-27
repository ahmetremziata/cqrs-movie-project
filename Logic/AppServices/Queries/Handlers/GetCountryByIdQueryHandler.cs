using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetCountryByIdQueryHandler : IQueryHandler<GetCountryByIdQuery, CountryResponse>
    {
        private readonly MovieDataContext _dataContext;

        public GetCountryByIdQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<CountryResponse> Handle(GetCountryByIdQuery query)
        {
            Country Country = await _dataContext.Countries.SingleOrDefaultAsync(item => item.Id == query.CountryId);
            if (Country == null)
            {
                return null;
            }
            
            return ConvertToDto(Country);
        }
        
        private CountryResponse ConvertToDto(Country Country)
        {
            return new CountryResponse
            {
                Id = Country.Id,
                Name = Country.Name
            };
        }
    }
}
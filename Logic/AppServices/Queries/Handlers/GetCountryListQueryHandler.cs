using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetCountryListQueryHandler : IQueryHandler<GetCountryListQuery, List<CountryResponse>>
    {
        private readonly MovieDataContext _dataContext;

        public GetCountryListQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<CountryResponse>> Handle(GetCountryListQuery query)
        {
            IReadOnlyList<Country> countries = await _dataContext.Countries.ToListAsync();
            List<CountryResponse> dtos = countries.Select(x => ConvertToDto(x)).OrderBy(item => item.Id).ToList();
            return dtos;
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
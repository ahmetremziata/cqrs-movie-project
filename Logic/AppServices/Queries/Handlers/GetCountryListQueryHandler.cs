using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetCountryListQueryHandler : IRequestHandler<GetCountryListQuery, List<CountryResponse>>
    {
        private readonly MovieDataContext _dataContext;

        public GetCountryListQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<CountryResponse>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Country> countries = await _dataContext.Countries.ToListAsync();
            List<CountryResponse> dtos = countries.Select(x => ConvertToDto(x)).OrderBy(item => item.Id).ToList();
            return dtos;
        }
        
        private CountryResponse ConvertToDto(Country country)
        {
            return new CountryResponse
            {
                Id = country.Id,
                Name = country.Name
            };
        }
        
    }
}
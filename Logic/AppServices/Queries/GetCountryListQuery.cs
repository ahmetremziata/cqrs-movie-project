using System.Collections.Generic;
using Logic.Responses;
using MediatR;

namespace Logic.AppServices.Queries
{
    public sealed class GetCountryListQuery : IRequest<List<CountryResponse>>
    {
    }
}
using Logic.Responses;
using MediatR;

namespace Logic.AppServices.Queries
{
    public sealed class GetCountryByIdQuery : IQuery<CountryResponse>
    {
        public int CountryId { get; set; }
    }
}
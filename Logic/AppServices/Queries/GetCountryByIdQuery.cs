using Logic.Responses;
using MediatR;

namespace Logic.AppServices.Queries
{
    public sealed class GetCountryByIdQuery : IRequest<CountryResponse>
    {
        public int CountryId { get; set; }
    }
}
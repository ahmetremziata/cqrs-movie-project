using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Responses;
using Logic.Utils;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetMovieByIdWithSeperateDomainModelQueryHandler : IQueryHandler<GetMovieListQuery, List<MovieResponse>>
    {
        private readonly ConnectionString _connection;
        
        public GetMovieByIdWithSeperateDomainModelQueryHandler(ConnectionString connection)
        {
            _connection = connection;
        }
        
        public async Task<List<MovieResponse>> Handle(GetMovieListQuery query)
        {
            throw new NotImplementedException();
        }
        
    }
}
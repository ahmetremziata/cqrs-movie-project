using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Dtos;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Logic.AppServices.Handlers
{
    public sealed class GetMoviePresentationListQueryHandler : IQueryHandler<GetMoviePresentationListQuery, MoviePresentationResponse>
    {
        private readonly IElasticClient _elasticClient;

        public GetMoviePresentationListQueryHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        
        public async Task<MoviePresentationResponse> Handle(GetMoviePresentationListQuery command)
        {
            MoviePresentationResponse response = new MoviePresentationResponse();
            var searchResponse = await _elasticClient
                .SearchAsync<Indexes.Movie>(i => i
                .Query(q => q.MatchAll())
                .From(command.Page - 1)
                .Size(command.Size));
            response.Page = command.Page;
            response.Size = command.Size;
            response.TotalElements = searchResponse.Total;
            response.TotalPages = searchResponse.Total % command.Size == 0 
                ? searchResponse.Total / command.Size 
                : searchResponse.Total / command.Size + 1;
            response.Movies = searchResponse.Documents.ToList();
            return response;
        }
    }
}
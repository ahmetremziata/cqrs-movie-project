using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Responses;
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
                .SearchAsync<Indexes.Movie>(s => s.Query(q =>
                        q.Raw(GetQueryUrl(command)))
                    .From(command.Page - 1)
                    .Size(command.Size)
                    .Sort(sort => sort.Descending(m => m.ConstructionYear)));           
            response.Page = command.Page;
            response.Size = command.Size;
            response.TotalElements = searchResponse.Total;
            response.TotalPages = searchResponse.Total % command.Size == 0 
                ? searchResponse.Total / command.Size 
                : searchResponse.Total / command.Size + 1;
            List<MovieSummaryResponse> movies = new List<MovieSummaryResponse>();

            foreach (var document in searchResponse.Documents.ToList())
            {
                movies.Add(new MovieSummaryResponse()
                {
                    MovieId = document.MovieId,
                    ConstructionYear = document.ConstructionYear,
                    Name = document.Name,
                    OriginalName = document.OriginalName,
                    PosterUrl = document.PosterUrl,
                    TotalMinute = document.TotalMinute,
                    VisionEntryDate = document.Identity.VisionEntryDate
                });
            }
            response.Movies = movies;
            return response;
        }

        private string GetQueryUrl(GetMoviePresentationListQuery command)
        {
            if (String.IsNullOrWhiteSpace(command.Name) && command.CountryId == null && command.TypeId == null && command.ConstructionYear == null)
            {
                return @"{""match_all"": {}}";
            }

            StringBuilder query = new StringBuilder();
            query.Append(@"{""bool"": {""should"": [");

            if (!String.IsNullOrWhiteSpace(command.Name))
            {
                query.Append(@"{""match_phrase_prefix"": {""name"": """ + command.Name + @"""}},");
                query.Append(@"{""match_phrase_prefix"": {""originalName"": """ + command.Name + @"""}}");
            }

            if ((command.CountryId != null || command.TypeId != null || command.ConstructionYear != null) && !String.IsNullOrWhiteSpace(command.Name))
            {
                query.Append(@",");
            }

            if (command.CountryId != null)
            {
                query.Append(@"{""match"": {""identity.countries.id"":" + command.CountryId + @"}}");
            }
            
            if ((command.TypeId != null || command.ConstructionYear != null) && command.CountryId != null)
            {
                query.Append(@",");
            }
            
            if (command.TypeId != null)
            {
                query.Append(@"{""match"": {""identity.types.id"":" + command.TypeId + @"}}");
            }
            
            if (command.ConstructionYear != null && command.TypeId != null)
            {
                query.Append(@",");
            }
            
            if (command.ConstructionYear != null)
            {
                query.Append(@"{""match"": {""constructionYear"":" + command.ConstructionYear + @"}}");
            }
            
            query.Append(@"]}}");

            return query.ToString();
        }
    }
}
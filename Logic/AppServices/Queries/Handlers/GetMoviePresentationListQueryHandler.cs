using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Responses;
using Nest;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetMoviePresentationListQueryHandler : IQueryHandler<GetMoviePresentationListQuery, MoviePresentationResponse>
    {
        private readonly IElasticClient _elasticClient;

        public GetMoviePresentationListQueryHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        
        public async Task<MoviePresentationResponse> Handle(GetMoviePresentationListQuery query)
        {
            MoviePresentationResponse response = new MoviePresentationResponse();
            var searchResponse = await _elasticClient
                .SearchAsync<Indexes.Movie>(s => s.Query(q =>
                        q.Raw(GetQueryUrl(query)))
                    .From(query.Page - 1)
                    .Size(query.Size)
                    .Sort(sort => sort.Descending(m => m.ConstructionYear)));           
            response.Page = query.Page;
            response.Size = query.Size;
            response.TotalElements = searchResponse.Total;
            response.TotalPages = searchResponse.Total % query.Size == 0 
                ? searchResponse.Total / query.Size 
                : searchResponse.Total / query.Size + 1;
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

        private string GetQueryUrl(GetMoviePresentationListQuery query)
        {
            if (String.IsNullOrWhiteSpace(query.Name) && query.CountryId == null && query.TypeId == null && query.ConstructionYear == null)
            {
                return @"{""match_all"": {}}";
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(@"{""bool"": {""must"": [");

            if (!String.IsNullOrWhiteSpace(query.Name))
            {
                stringBuilder.Append(@"{""match_phrase_prefix"": {""name"": """ + query.Name + @"""}}");
            }

            if ((query.CountryId != null || query.TypeId != null || query.ConstructionYear != null) && !String.IsNullOrWhiteSpace(query.Name))
            {
                stringBuilder.Append(@",");
            }

            if (query.CountryId != null)
            {
                stringBuilder.Append(@"{""match"": {""identity.countries.id"":" + query.CountryId + @"}}");
            }
            
            if ((query.TypeId != null || query.ConstructionYear != null) && query.CountryId != null)
            {
                stringBuilder.Append(@",");
            }
            
            if (query.TypeId != null)
            {
                stringBuilder.Append(@"{""match"": {""identity.types.id"":" + query.TypeId + @"}}");
            }
            
            if (query.ConstructionYear != null && query.TypeId != null)
            {
                stringBuilder.Append(@",");
            }
            
            if (query.ConstructionYear != null)
            {
                stringBuilder.Append(@"{""match"": {""constructionYear"":" + query.ConstructionYear + @"}}");
            }
            
            stringBuilder.Append(@"]}}");

            return stringBuilder.ToString();
        }
    }
}
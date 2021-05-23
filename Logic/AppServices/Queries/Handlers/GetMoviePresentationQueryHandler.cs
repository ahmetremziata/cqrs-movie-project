using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetMoviePresentationQueryHandler : IQueryHandler<GetMoviePresentationQuery, Logic.Indexes.Movie>
    {
        private readonly IElasticClient _elasticClient;

        public GetMoviePresentationQueryHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        
        public async Task<Logic.Indexes.Movie> Handle(GetMoviePresentationQuery query)
        {
            var searchResponse = await _elasticClient
                .SearchAsync<Indexes.Movie>(s => s.Query(q =>
                        q.Raw(GetQueryUrl(query))));           
           
            return searchResponse.Documents.ToList().First();
        }
        
        private string GetQueryUrl(GetMoviePresentationQuery query)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(@"{""bool"": {""should"": [");
            stringBuilder.Append(@"{""match"": {""movieId"":" + query.MovieId + @"}}");
            stringBuilder.Append(@"]}}");
            return stringBuilder.ToString();
        }
    }
}
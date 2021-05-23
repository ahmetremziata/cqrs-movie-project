using System.Collections.Generic;

namespace Logic.Responses
{
    public class MoviePresentationResponse
    {
        public List<MovieSummaryResponse> Movies { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public long TotalElements { get; set; }
        public long TotalPages { get; set; }
    }
}
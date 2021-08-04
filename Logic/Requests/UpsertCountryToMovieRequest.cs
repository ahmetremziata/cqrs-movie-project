using System.Collections.Generic;

namespace Logic.Requests
{
    public class UpsertCountryToMovieRequest
    {
        public List<int> CountryIds { get; set; }
    }
}
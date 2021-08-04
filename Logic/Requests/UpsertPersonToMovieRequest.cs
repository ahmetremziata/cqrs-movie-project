using System.Collections.Generic;
using Logic.Responses;

namespace Logic.Requests
{
    public class UpsertPersonToMovieRequest
    {
        public List<MoviePersonRequest> MoviePersons { get; set; }
    }
}
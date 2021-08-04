using System.Collections.Generic;
using Logic.Requests;
using Logic.Responses;

namespace Logic.AppServices.Commands
{
    public sealed class UpsertPersonToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public List<MoviePersonRequest> MoviePersons { get; set; }
    }
}
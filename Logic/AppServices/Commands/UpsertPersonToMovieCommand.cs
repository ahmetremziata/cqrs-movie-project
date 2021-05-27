using System.Collections.Generic;
using Logic.Responses;

namespace Logic.AppServices.Commands
{
    public sealed class UpsertPersonToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public List<MoviePersonResponse> MoviePersons { get; set; }
    }
}
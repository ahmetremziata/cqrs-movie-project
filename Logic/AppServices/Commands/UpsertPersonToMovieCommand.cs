using System.Collections.Generic;
using Logic.Dtos;

namespace Logic.AppServices.Commands
{
    public sealed class UpsertPersonToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public List<MoviePersonDto> MoviePersons { get; set; }
    }
}
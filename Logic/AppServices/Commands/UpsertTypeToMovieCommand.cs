using System.Collections.Generic;
using Logic.Dtos;

namespace Logic.AppServices.Commands
{
    public sealed class UpsertTypeToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public List<int> TypeIds { get; set; }
    }
}
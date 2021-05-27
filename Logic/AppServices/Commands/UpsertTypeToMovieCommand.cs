using System.Collections.Generic;

namespace Logic.AppServices.Commands
{
    public sealed class UpsertTypeToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public List<int> TypeIds { get; set; }
    }
}
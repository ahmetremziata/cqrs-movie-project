using System.Collections.Generic;

namespace Logic.AppServices.Commands
{
    public sealed class UpsertCountryToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public List<int> CountryIds { get; set; }
    }
}
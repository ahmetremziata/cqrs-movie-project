namespace Logic.AppServices.Commands
{
    public class InsertCountryToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public int CountryId { get; set; }
    }
}
namespace Logic.AppServices.Commands
{
    public sealed class RemoveCountryFromMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public int CountryId { get; set; }
    }
}
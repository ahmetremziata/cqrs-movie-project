namespace Logic.AppServices.Commands
{
    public sealed class SynchronizeMovieCommand : ICommand
    {
        public int MovieId { get; set; }
    }
}
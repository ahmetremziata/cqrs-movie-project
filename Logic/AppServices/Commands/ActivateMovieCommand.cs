namespace Logic.AppServices.Commands
{
    public sealed class ActivateMovieCommand : ICommand
    {
        public int MovieId { get; set; }
    }
}
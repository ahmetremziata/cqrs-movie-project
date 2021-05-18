namespace Logic.AppServices.Commands
{
    public sealed class DeactivateMovieCommand : ICommand
    {
        public int MovieId { get; set; }
    }
}
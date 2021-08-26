namespace Logic.AppServices.Commands
{
    public sealed class RemoveTypeFromMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public int TypeId { get; set; }
    }
}
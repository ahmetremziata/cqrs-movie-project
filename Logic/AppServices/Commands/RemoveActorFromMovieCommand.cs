namespace Logic.AppServices.Commands
{
    public sealed class RemoveActorFromMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public int RoleId { get; set; }
    }
}
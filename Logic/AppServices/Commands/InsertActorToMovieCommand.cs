namespace Logic.AppServices.Commands
{
    public class InsertActorToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public int RoleId { get; set; }
        public string CharacterName { get; set; }
    }
}
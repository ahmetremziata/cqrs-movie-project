namespace Logic.AppServices.Commands
{
    public class InsertTypeToMovieCommand : ICommand
    {
        public int MovieId { get; set; }
        public int TypeId { get; set; }
    }
}
namespace Logic.AppServices.Commands
{
    public sealed class DeleteMovieCommand : ICommand
    {
        public int Id { get; set; }
    }
}
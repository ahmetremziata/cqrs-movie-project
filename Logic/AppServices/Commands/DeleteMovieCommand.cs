namespace Logic.AppServices
{
    public sealed class DeleteMovieCommand : ICommand
    {
        public int Id { get; set; }
    }
}
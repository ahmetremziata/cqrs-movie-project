namespace Logic.AppServices.Commands
{
    public sealed class DeletePersonCommand : ICommand
    {
        public int Id { get; set; }
    }
}
namespace Logic.AppServices.Commands
{
    public sealed class DeleteTypeCommand : ICommand
    {
        public int TypeId { get; set; }
    }
}
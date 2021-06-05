namespace Logic.AppServices.Commands
{
    public sealed class EditTypeInfoCommand : ICommand
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
    }
}
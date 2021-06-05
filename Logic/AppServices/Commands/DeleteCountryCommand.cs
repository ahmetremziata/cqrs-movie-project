namespace Logic.AppServices.Commands
{
    public sealed class DeleteCountryCommand : ICommand
    {
        public int CountryId { get; set; }
    }
}
namespace Logic.AppServices.Commands
{
    public class EditCountryInfoCommand : ICommand
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Logic.Data.Entities
{
    public class MovieCountry
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CountryId { get; set; }
    }
}
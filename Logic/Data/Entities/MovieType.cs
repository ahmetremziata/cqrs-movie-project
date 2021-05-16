using System.ComponentModel.DataAnnotations;

namespace Logic.Data.Entities
{
    public class MovieType
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int TypeId { get; set; }
    }
}
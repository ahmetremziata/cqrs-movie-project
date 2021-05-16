using System.ComponentModel.DataAnnotations;

namespace Logic.Data.Entities
{
    public class MoviePerson
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public int RoleId { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.Data.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
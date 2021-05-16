using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.Data.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Biography { get; set; }
        public string AvatarUrl { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string DeathDate { get; set; }
        public string DeathPlace { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
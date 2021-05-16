using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.Data.Entities
{
    public class Type
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
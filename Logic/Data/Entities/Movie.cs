using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.Data.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Description { get; set; }
        public int ConstructionYear { get; set; }
        public int TotalMinute { get; set; }
        public string PosterUrl { get; set; }
        public DateTime? VisionEntryDate { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
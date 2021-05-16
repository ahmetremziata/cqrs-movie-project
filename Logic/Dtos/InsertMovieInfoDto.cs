using System;
using System.Collections.Generic;

namespace Logic.Dtos
{
    public class InsertMovieInfoDto
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Description { get; set; }
        public int ConstructionYear { get; set; }
        public int TotalMinute { get; set; }
        public string PosterUrl { get; set; }
        public DateTime? VisionEntryDate { get; set; }
        
        public List<int> TypeIds { get; set; }
    }
}
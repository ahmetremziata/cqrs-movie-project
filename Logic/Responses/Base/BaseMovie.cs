using System;

namespace Logic.Responses.Base
{
    public class BaseMovie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public int ConstructionYear { get; set; }
        public int TotalMinute { get; set; }
        public string PosterUrl { get; set; }
        public DateTime? VisionEntryDate { get; set; }
    }
}